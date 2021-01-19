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
            //check unique columns
            var result = await _context.Reports.AnyAsync(u => u.ID == report.ID);

            if (result)
            {
                return null;
            }

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

        public async Task<Report> Get(Guid id)
        {
            var query = from u in _context.Reports
                        where Guid.Equals(u.ID, id)
                        select u;

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Report> Get(string city)
        {
            var query = from r in _context.Reports
                        where r.City == city
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
    }
}