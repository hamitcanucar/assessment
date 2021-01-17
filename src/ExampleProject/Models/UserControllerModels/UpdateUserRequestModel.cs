using System;
using System.ComponentModel.DataAnnotations;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels.UserControllerModels
{
    public class UpdateUserRequestModel : AControllerEntityModel<UserModel>
    {
        [MinLength(11), MaxLength(11)]
        public string PersonalId { get; set; }

        [EmailAddress, MaxLength(64)]
        public string Email { get; set; }

        [MinLength(4), MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(32)]
        public string Phone { get; set; }

        public UserType? UserType { get; set; }
        public bool? Active { get; set; }

        public override UserModel ToModel()
        {
            return new UserModel
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                UserType = UserType
            };
        }
    }
}