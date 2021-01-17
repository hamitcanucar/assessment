using System.ComponentModel.DataAnnotations;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels.UserControllerModels
{
    public class RegisterRequestModel : AControllerEntityModel<User>
    {
        [EmailAddress, Required, MaxLength(64)]
        public string Email { get; set; }

        [Required, MinLength(4), MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string FirstName { get; set; }

        [MaxLength(64)]
        public string LastName { get; set; }

        public override User ToModel()
        {
            return new User
            {
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
            };
        }
    }
}