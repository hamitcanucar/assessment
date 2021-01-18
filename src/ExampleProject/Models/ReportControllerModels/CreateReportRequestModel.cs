using System.ComponentModel.DataAnnotations;

namespace ExampleProject.Models.ControllerModels.ReportControllerModels
{
    public class CreateReportRequestModel
    {
        [Required, MinLength(4), MaxLength(128)]
        public string City { get; set; }
    }
}