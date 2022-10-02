using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using System.Net;
using Application.Dto.User;
using Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Presentation.ViewModels.User;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserViewModel), (int)HttpStatusCode.Created)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var userDto = _mapper.Map<UserDto>(model);

            await _userService.RegisterAsync(userDto, model.Password!);

            return Created($"users/{userDto.Id}", null);
        }

        [ModelStateValidation]
        [HttpPost("token")]
        [ProducesResponseType(typeof(TokenViewModel), (int)HttpStatusCode.OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Token([FromBody] GetTokenViewModel model)
        {
            var userDto = new UserDto
            {
                Email = model.EmailOrUsername!,
                Username = model.EmailOrUsername!
            };

            var token = await _userService.GetTokenAsync(userDto, model.Password!);

            var resp = _mapper.Map<TokenViewModel>(userDto);

            resp.AccessToken = token;

            return Ok(resp);
        }
    }
}