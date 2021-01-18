using System;
using System.Collections.Generic;

namespace ExampleProject.Models.ViewModels
{
    public class CreateReportViewModel
    {
        public CreateReportViewModel()
        {
            ReportCreateDate = DateTime.Now;
        }
        public string City { get; set; }
    }
}