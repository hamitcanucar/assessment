using System;
using System.ComponentModel.DataAnnotations;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels.ReportControllerModels
{
    public class CreateReportRequestModel : AControllerEntityModel<Report>
    {
        [Required, MinLength(4), MaxLength(128)]
        public string City { get; set; }

        public override Report ToModel()
        {
            return new Report
            {
                City = City,
                ReportCreateTime = DateTime.Now
            };
        }
    }

   
}