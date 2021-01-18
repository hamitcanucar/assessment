using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleProject.Models;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Services.Abstract
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<UserModel> Login(string pidOrEmail, string password);
        Task<User> Get(Guid id);
        Task<User> Get(string pidOrEmail);
        Task<ICollection<User>> Get();
        Task<User> Update(Guid id, UserModel user, string password = null);
        Task<ICollection<UserInformation>> GetUserInformations(Guid userId);
        Task<UserInformation> UserInformation(UserInformation userInformation, Guid id);

        Task<ICollection<Report>> GetReports();
        Task<bool> UpdatePassword(User user, string password);
        Task<User> UpdatePassword(Guid id, string oldPassword, string newPassword);
        
    }
}