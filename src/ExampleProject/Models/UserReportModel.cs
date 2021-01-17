using System;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models
{
    public class UserReportModel : AEntityModel<UserReport, UserReportModel>
    {
        public UserReportModel()
        {
            ID = Guid.NewGuid();
        }

        public DateTime ReportCreateTime { get; set; }
        public UserReportType UserReportType { get; set; }

        public override void SetValuesFromEntity(UserReport entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            ReportCreateTime = entity.ReportCreateTime;
            UserReportType = entity.UserReportType;

        }

        public override UserReport ToEntity()
        {
            return new UserReport
            {
                ID = ID,
                ReportCreateTime = ReportCreateTime,
                UserReportType = UserReportType
            };
        }
    }

    public static class UserReportEntityExtentions
    {
        public static UserReportModel ToModel(this UserReport userReport)
        {
            var model = new UserReportModel();
            model.SetValuesFromEntity(userReport);
            return model;
        }
    }
}