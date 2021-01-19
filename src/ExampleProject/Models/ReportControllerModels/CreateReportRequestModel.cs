using System;
using System.ComponentModel.DataAnnotations;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Models.ControllerModels.ReportControllerModels
{
    public class CreateReportRequestModel : AControllerEntityModel<Report>
    {
        [Required, MinLength(4), MaxLength(128)]
        public string Location { get; set; }

        public override Report ToModel()
        {
            return new Report
            {
                Location = Location
            };
        }
    }

   
}