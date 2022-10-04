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
            var userId = idModel.id!;

            if ((bool)status)
            {
                await _userService.ApproveAsync(userId);
            }
            else
            {
                await _userService.RejectAsync(userId);
            }

            return Ok();
        }

        [ModelStateValidation]
        [HttpGet]
        [ProducesResponseType(typeof(ListViewModelResponse<UserViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] SearchUserViewModel model)
        {
            var resp = await _userService.SearchAsync(model.Offset, model.Limit, model.SearchText);

            return Ok(_mapper.Map<ListViewModelResponse<UserViewModel>>(resp));
        }

    }
}