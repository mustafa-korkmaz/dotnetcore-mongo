using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using System.Net;
using Application.Constants;
using Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Presentation.ViewModels;
using Presentation.ViewModels.User;

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize(AppConstants.AdminAuthorizationPolicy)]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpPut("{id}/status")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> SetAccountConfirmationStatus([FromRoute] ObjectIdViewModel idModel,
            [FromBody] StatusViewModel statusModel)
        {
            var status = statusModel.Value!;

            if ((bool)status)
            {
                await _userService.ApproveAsync(idModel.id!);
            }
            else
            {
                await _userService.RejectAsync(idModel.id!);
            }

            return Ok();
        }
    }
}