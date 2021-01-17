using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Models.ControllerModels.UserControllerModels
{
    public class UpdatePasswordRequestModel
    {
        [Required, MinLength(4), MaxLength(128)]
        public string OldPassword { get; set; }

        [Required, MinLength(4), MaxLength(128)]
        public string NewPassword { get; set; }
    }
}