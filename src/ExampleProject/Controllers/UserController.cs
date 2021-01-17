using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleProject.Models;
using ExampleProject.Models.ControllerModels;
using ExampleProject.Models.ControllerModels.UserControllerModels;
using ExampleProject.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Controllers
{
    [Route("user")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class UserController : AController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<GenericResponse<UserModel>> Register([FromBody] RegisterRequestModel model)
        {
            var user = model.ToModel();
            var result = await _userService.Register(user);

            if (result == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }

            return new GenericResponse<UserModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<GenericResponse<UserModel>> Login([FromBody] LoginRequestModel model)
        {
            var result = await _userService.Login(model.PIDorEmail, model.Password);

            if (result == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.LOGIN_WRONG_CRIDENTIALS),
                    Message = ErrorMessages.LOGIN_WRONG_CRIDENTIALS
                };
            }
            else if (result.Token == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND,
                    Data = result
                };
            }

            return new GenericResponse<UserModel>
            {
                Success = true,
                Data = result
            };
        }

        [HttpPost]  
        [Route("userInformation")]
        [Authorize]
        public async Task<GenericResponse<UserInformationModel>> UserInformation([FromBody] UserInformationRequestModel model)
        {
            var userId = GetUserIdFromToken();
            var userInformation = model.ToModel();
            var result = await _userService.UserInformation(userInformation, userId);

            if (result == null)
            {
                return new GenericResponse<UserInformationModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }

            return new GenericResponse<UserInformationModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpGet]
        [Route("getUserInformaation")]
        [Authorize]
        public async Task<IEnumerable<UserInformationModel>> GetUserInformation()
        {
            var userId = GetUserIdFromToken();

            var result = await _userService.GetUserInformations(userId);

            return result.Select(u => u.ToModel());
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<UserModel> GetUser(Guid id)
        {
            var user = await _userService.Get(id);

            if (user == null)
            {
                return null;
            }
            else
            {
                return user.ToModel();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<UserModel>> GetUserList()
        {
            var result = await _userService.Get();
            return result.Select(u => u.ToModel());
        }

        [HttpPatch]
        [Authorize]
        public async Task<GenericResponse<UserModel>> UpdateUser([FromBody] UpdateUserRequestModel model)
        {
            var result = await _userService.Update(GetUserIdFromToken(), model.ToModel(), model.Password);

            if (result == null)
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }
            else if (Guid.Equals(Guid.Empty, result.ID))
            {
                return new GenericResponse<UserModel>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };
            }

            return new GenericResponse<UserModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpPatch]
        [Route("password")]
        [Authorize]
        public async Task<GenericResponse<string>> UpdatePassword([FromBody] UpdatePasswordRequestModel model)
        {
            var result = await _userService.UpdatePassword(GetUserIdFromToken(), model.OldPassword, model.NewPassword);

            if (result == null)
                return new GenericResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_NOT_FOUND),
                    Message = ErrorMessages.USER_NOT_FOUND
                };
            if (result.ID == Guid.Empty)
                return new GenericResponse<string>
                {
                    Code = nameof(ErrorMessages.USER_UPDATE_WRONG_OLDPASS),
                    Message = ErrorMessages.USER_UPDATE_WRONG_OLDPASS
                };

            return new GenericResponse<string> { Success = true };
        }
    }
}