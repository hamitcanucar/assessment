using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ExampleProject.Models;
using ExampleProject.Util;
using ExampleProject.Context;
using ExampleProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Collections.Generic;
using ExampleProject.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System.IO;
using ExampleProject.Models.ControllerModels;
using ExampleProject.Models.ReportControllerModels;

namespace ExampleProject.Services.Concrete
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ReportService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Report> CreateReport(Report report)
        {
            report.ReportType = ReportType.GettingReady;
            _context.Add(report);
            await _context.SaveChangesAsync();

            return report;
        }

        public async Task<ICollection<Report>> GetReports()
        {
            var query = from r in _context.Reports
                        select r;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<ReportViewModel> GetReportDetailFromId(Guid id)
        {
            var report = _context.Reports.FirstOrDefaultAsync(q => q.ID == id).Result;
            //Group User With UserId and Location Find Total User
            var totalUser = from user in _context.UserInformations
                            where user.Location == report.Location
                            group user by new { user.Location ,user.UserId} into g
                            select g.Count();
            //Group User With Phone and Location Find Total User
            var totalPhone = from user in _context.UserInformations
                            where user.Location == report.Location
                            group user by new { user.Location ,user.Phone} into g
                           select g.Count();

            return new ReportViewModel
            {
                Locaiton= report.Location,
                TotalUserCount= totalUser,
                TotalPhoneCount= totalPhone,
            };
        }

        public async Task<Report> Get(Guid id)
        {
            var query = from u in _context.Reports
                        where Guid.Equals(u.ID, id)
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Report> Get(string location)
        {
            var query = from r in _context.Reports
                        where r.Location == location
                        select r;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Report> GetReportFromId(Guid id)
        {
            var query = from r in _context.Reports
                        where r.ID == id
                        select r;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ICollection<Report>> Get()
        {
            var query = from r in _context.Reports
                        select r;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IList<Report>> ApproveReports(string[] guids)
        {
            var reportList = _context.Reports.Where(x=>guids.Contains(x.Guid));

            reportList.ForEach(x=>x.ReportType == ReportType.Complete);

          _context.Reports.UpdateRange(reportList);
          _context.SaveChangesAsync();  
           return reportList;
        } 
    }
}