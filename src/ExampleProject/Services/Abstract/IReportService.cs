using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleProject.Models;
using ExampleProject.DataAccess.Entities;
using ExampleProject.Models.ControllerModels;
using ExampleProject.Models.ReportControllerModels;

namespace ExampleProject.Services.Abstract
{
    public interface IReportService
    {
        Task<Report> CreateReport(Report report);
        Task<Report> GetReportFromId(Guid id);
        Task<ReportViewModel> GetReportDetailFromId(Guid id);
        Task<ICollection<Report>> GetReports();
        Task<IList<Report>> ApproveReports(string[] guids );
    }
}