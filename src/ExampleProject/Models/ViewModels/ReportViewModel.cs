using System;
using System.Collections.Generic;

namespace ExampleProject.Models.ViewModels
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            ReportCreateDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime ReportCreateDate { get; set; }
        public int TotalUserCount { get; set; }
        public int TotalPhoneCount { get; set; }
        public ICollection<UserInformation> UserInformation { get; set; }
    }

    public class UserInformation
    {
        public UserInformation userInformation { get; set; }
        public int phoneCount { get; set; }
    }
}