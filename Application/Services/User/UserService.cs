using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using Application.Constants;
using Application.Dto.User;
using Application.Exceptions;
using AutoMapper;
using Domain.Aggregates.User;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.User
{
    public class UserService : ServiceBase<IUserRepository, Domain.Aggregates.User.User, UserDto>, IUserService
    {
        public UserService(IUnitOfWork uow, ILogger<UserService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }

        public async Task<string> GetTokenAsync(UserDto userDto, string password)
        {
            Domain.Aggregates.User.User? user;

            if (userDto.Username.Contains("@")) // login via email
            {
                user = await Repository.GetByEmailAsync(userDto.Email.GetNormalized()!);
            }
            else // login via username
            {
                user = await Repository.GetByUsernameAsync(userDto.Username.GetNormalized()!);
            }

            if (user == null)
            {
                throw new ValidationException(ErrorMessages.UserNotFound);
            }

            var isPasswordValid = VerifyHashedPassword(user.PasswordHash, password);

            if (!isPasswordValid)
            {
                throw new ValidationException(ErrorMessages.IncorrectUsernameOrPassword);
            }

            userDto.Id = user.Id;
            userDto.Username = user.Username;
            userDto.Email = user.Email;
            userDto.NameSurname = user.NameSurname;
            userDto.CreatedAt = user.CreatedAt;
            userDto.Claims = user.Claims.ToList();

            return GenerateToken(userDto);
        }

        public async Task RegisterAsync(UserDto userDto, string password)
        {
            var userByName = await Repository.GetByEmailAsync(userDto.Email);

            if (userByName != null)
            {
                throw new ValidationException(ErrorMessages.UserExists);
            }

            var userByEmail = await Repository.GetByEmailAsync(userDto.Email.Normalize());

            if (userByEmail != null)
            {
                throw new ValidationException(ErrorMessages.UserExists);
            }

            // userDto.Claims = new List<string> { "app_user" };

            var user = Mapper.Map<Domain.Aggregates.User.User>(userDto);

            user.SetPasswordHash(HashPassword(password));

            await Repository.InsertOneAsync(user);

            userDto.Id = user.Id;
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;

            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }

        private string GenerateToken(UserDto user)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();

            var emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String);

            claims.Add(new Claim("id", user.Id));
            claims.Add(new Claim("username", user.Username));

            claims.Add(emailClaim);

            foreach (var userClaim in user.Claims)
            {
                claims.Add(new Claim("claims", userClaim));
            }

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), claims);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenConstants.Issuer,
                Audience = JwtTokenConstants.Audience,
                SigningCredentials = JwtTokenConstants.SigningCredentials,
                Subject = identity,
                Expires = DateTime.UtcNow.Add(JwtTokenConstants.TokenExpirationTime),
                NotBefore = DateTime.UtcNow
            });

            return handler.WriteToken(securityToken);
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }

}
