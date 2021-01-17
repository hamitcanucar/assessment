using System;
using ExampleProject.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Controllers
{
    public abstract class AController<T> : ControllerBase
    {
        protected readonly ILogger _logger;

        protected AController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected Guid GetUserIdFromToken()
        {
            Guid userId = Guid.Empty;
            try
            {
                if (!Guid.TryParse(User.FindFirst(JWTUser.ID).Value, out userId))
                {
                    _logger.LogWarning("Wrong Guid format in token ({0})", JWTUser.ID);
                }
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Use {0}() method in Authorize methods!", nameof(GetUserIdFromToken));
            }
            return userId;
        }

        protected UserType GetUserTypeFromToken()
        {
            UserType userType = UserType.User;
            try
            {
                if (!Enum.TryParse<UserType>(User.FindFirst(System.Security.Claims.ClaimTypes.Role).Value, out userType))
                {
                    _logger.LogWarning("Wrong UserType format in token (role)");
                }
            }
            catch (System.Exception)
            {
                _logger.LogWarning("Use {0}() method in Authorize methods!", nameof(GetUserTypeFromToken));
            }
            return userType;
        }
    }
}