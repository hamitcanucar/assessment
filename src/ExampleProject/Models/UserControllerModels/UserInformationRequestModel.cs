using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels.UserControllerModels
{
    public class UserInformationRequestModel : AControllerEntityModel<UserInformation>
    {
        public string PersonalId { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }

        

        public override UserInformation ToModel()
        {
            return new UserInformation
            {
                PersonalId = PersonalId,
                Phone = Phone,
                Location = Location,
                Email = Email,
                Content = Content
            };
        }
    }
}