using System;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models
{
    public class UserInformationModel : AEntityModel<UserInformation, UserInformationModel>
    {
        public UserInformationModel()
        {
            ID = Guid.NewGuid();
        }

        public string PersonalId { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public override void SetValuesFromEntity(UserInformation entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            PersonalId = entity.PersonalId;
            City = entity.City;
            Phone = entity.Phone;

        }

        public override UserInformation ToEntity()
        {
            return new UserInformation
            {
                ID = ID,
                PersonalId = PersonalId,
                City = City,
                Phone = Phone
            };
        }
    }

    public static class UserInformationEntityExtentions
    {
        public static UserInformationModel ToModel(this UserInformation userInformation)
        {
            var model = new UserInformationModel();
            model.SetValuesFromEntity(userInformation);
            return model;
        }
    }
}