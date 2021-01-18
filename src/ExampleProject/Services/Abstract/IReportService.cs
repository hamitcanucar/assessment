using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleProject.Models;
using ExampleProject.DataAccess.Entities;

namespace ExampleProject.Services.Abstract
{
    public interface IReportService
    {
        Task<Report> CreateReport(Report report);
        Task<Report> GetReportFromId(Guid id);
        Task<ICollection<Report>> GetReports();
    }
}