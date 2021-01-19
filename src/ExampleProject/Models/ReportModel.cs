using System;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models
{
    public class ReportModel : AEntityModel<Report, ReportModel>
    {
        public ReportModel()
        {
            ID = Guid.NewGuid();
        }

        public DateTime ReportCreateTime { get; set; }
        public ReportType ReportType { get; set; }
        public string Location { get; set; }

        public override void SetValuesFromEntity(Report entity)
        {
            if (entity == null) return;

            base.SetValuesFromEntity(entity);

            ID = entity.ID;
            ReportCreateTime = entity.ReportCreateTime;
            ReportType = entity.ReportType;
            Location = entity.Location;
        }

        public override Report ToEntity()
        {
            return new Report
            {
                ID = ID,
                ReportCreateTime = ReportCreateTime,
                ReportType = ReportType,
                Location = Location
            };
        }
    }

    public static class UserReportEntityExtentions
    {
        public static ReportModel ToModel(this Report userReport)
        {
            var model = new ReportModel();
            model.SetValuesFromEntity(userReport);
            return model;
        }
    }
}