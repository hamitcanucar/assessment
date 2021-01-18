using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels.UserControllerModels
{
    public class UserInformationRequestModel : AControllerEntityModel<UserInformation>
    {
        public string PersonalId { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public override UserInformation ToModel()
        {
            return new UserInformation
            {
                PersonalId = PersonalId,
                Phone = Phone,
                City = City
            };
        }
    }
}