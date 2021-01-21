using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Models;
using ExampleProject.Util;
using ExampleProject.Context;
using ExampleProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Collections.Generic;
using ExampleProject.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
    [HttpPost]
        public async Task<UserModel> Login(string email, string password)
        {
            var hashedPass = password.HashToSha256();

            var query = from u in _context.Users
                        where (u.Email == email) && u.Password == hashedPass
                        select u;

            var user = await query.AsNoTracking().FirstOrDefaultAsync();

            if (user == null)
                return null;

            var result = user.ToModel();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(JWTUser.ID, result.ID.ToString()),
                        new Claim(ClaimTypes.Role, result.UserType.ToString()),
                    }),
                Issuer = "false", //_appSettings.Issuer,
                Expires = DateTime.UtcNow.AddHours(1), //DateTime.UtcNow.AddMinutes(_appSettings.JwtValidityInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Token = tokenHandler.WriteToken(token);


            return result;
        }
 [HttpPost]
        public async Task<User> Register(User user)
        {
            //check unique columns
            var result = await _context.Users.AnyAsync(u => u.Email == user.Email);

            if (result)
            {
                return null;
            }

            user.Password = user.Password.HashToSha256();
            user.UserType = UserType.User;

            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
 [HttpPost]
        public async Task<UserInformation> UserInformation(UserInformation userInformation, Guid id)
        {
            userInformation.UserId = id;

            _context.Add(userInformation);
            await _context.SaveChangesAsync();
            return userInformation;
        }

        public async Task<ICollection<UserInformation>> GetUserInformations(Guid userId)
        {
            var query = from u in _context.UserInformations
                        where u.UserId == userId
                        select u;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Report>> GetReports()
        {
            var query = from r in _context.Reports
                        select r;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<UserInformation> GetUserInformation(Guid userInformationId)
        {
            var query = from u in _context.UserInformations
                        where u.ID == userInformationId
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<UserInformation> DeleteUserInformation(Guid userInformationId)
        {
            var entity = await GetUserInformation(userInformationId);
            return await DeleteUserInformation(entity);
        }
[HttpPost]
        private async Task<UserInformation> DeleteUserInformation(UserInformation userInformation)
        {
            if (userInformation == null) return null;

            _context.UserInformations.Remove(userInformation);
            await _context.SaveChangesAsync();
            return userInformation;
        }

        public async Task<User> Get(Guid id)
        {
            var query = from u in _context.Users
                        where Guid.Equals(u.ID, id)
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<User> Get(string email)
        {
            var query = from u in _context.Users
                        where u.Email == email
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> Get()
        {
            var query = from u in _context.Users
                        select u;

            return await query.AsNoTracking().ToListAsync();
        }
        
        [HttpDelete]
        public async Task<User> DeleteUser(string email)
        {
            var entity = await Get(email);
            return await DeleteUser(entity);
        }

        private async Task<User> DeleteUser(User user)
        {
            if (user == null) return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        [HttpPatch]
        public async Task<User> Update(Guid id, UserModel model, string password = null)
        {
            var result = await _context.Users.Where(u => Guid.Equals(u.ID, id)).FirstOrDefaultAsync();

            if (result == null)
                return new User { ID = Guid.Empty };

            result = await SetUpdateValues(result, model, password);
            if (result == null) return null;

            await _context.SaveChangesAsync();
            return result;
        }

        [HttpPost]
        private async Task<User> SetUpdateValues(User user, UserModel model, string password = null)
        {
            if (!String.IsNullOrEmpty(model.Email))
            {
                //check unique columns
                var query = from u in _context.Users
                            where u.Email == model.Email
                            select new
                            {
                                u.Email
                            };
                var conflicts = await query.AsNoTracking().ToListAsync();

                foreach (var c in conflicts)
                {
                    if (c.Email == model.Email && user.Email != model.Email)
                    {
                        return null;
                    }
                }
            }

            user.Email = (String.IsNullOrEmpty(model.Email)) ? user.Email : model.Email;
            user.Password = (String.IsNullOrEmpty(password)) ? user.Password : password.HashToSha256();
            user.FirstName = (String.IsNullOrEmpty(model.FirstName)) ? user.FirstName : model.FirstName;
            user.LastName = (String.IsNullOrEmpty(model.LastName)) ? user.LastName : model.LastName;

            return user;
        }

        [HttpPatch]
        public async Task<bool> UpdatePassword(User user, string password)
        {
            _context.Users.Attach(user);
            user.Password = password.HashToSha256();
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        [HttpPatch]
        public async Task<User> UpdatePassword(Guid id, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.ID == id);
            if (user == null) return null;

            if (oldPassword.HashToSha256() != user.Password) return new User { ID = Guid.Empty };
            user.Password = newPassword.HashToSha256();
            await _context.SaveChangesAsync();
            return user;
        }
    }
}