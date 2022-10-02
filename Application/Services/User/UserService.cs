using Application.Dto.User;
using AutoMapper;
using Domain.Aggregates.User;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.User
{
    public class UserService : ServiceBase<IUserRepository, Domain.Aggregates.User.User, UserDto>
    {
        public UserService(IUnitOfWork uow, ILogger<UserService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }
    }

}
