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
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }

        public override void SetValuesFromEntity(UserInformation entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            PersonalId = entity.PersonalId;
            Location = entity.Location;
            Phone = entity.Phone;
            Email = entity.Email;
            Content = entity.Content;

        }

        public override UserInformation ToEntity()
        {
            return new UserInformation
            {
                ID = ID,
                PersonalId = PersonalId,
                Location = Location,
                Phone = Phone,
                Content = Content
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