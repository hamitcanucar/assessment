using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleProject.DataAccess.Entities;
using ExampleProject.Models;
using ExampleProject.Models.ControllerModels;
using ExampleProject.Models.ControllerModels.ReportControllerModels;
using ExampleProject.Models.ControllerModels.UserControllerModels;
using ExampleProject.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Controllers
{
    [Route("report")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class ReportController : AController<ReportController>
    {
        private readonly IReportService _reportService;

        public ReportController(ILogger<ReportController> logger, IReportService reportService) : base(logger)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Route("createReport")]
        [Authorize]
        public async Task<GenericResponse<ReportModel>> CreateReport([FromBody] CreateReportRequestModel model)
        {
            var report = model.ToModel();
            var result = await _reportService.CreateReport(report);

            if (result == null)
            {
                return new GenericResponse<ReportModel>
                {
                    Code = nameof(ErrorMessages.DUPLICATED_CRIDENTIAL),
                    Message = ErrorMessages.DUPLICATED_CRIDENTIAL
                };
            }

            return new GenericResponse<ReportModel>
            {
                Success = true,
                Data = result.ToModel()
            };
        }

        [HttpGet]
        [Route("getReport/{id}")]
        [Authorize]
        public async Task<ReportModel> GetReport(Guid id)
        {
            var report = await _reportService.GetReportFromId(id);

            if (report == null)
            {
                return null;
            }
            else
            {
                return report.ToModel();
            }
        }

        [HttpGet]
        [Route("getReportList")]
        [Authorize]
        public async Task<IEnumerable<ReportModel>> GetreportList()
        {
            var result = await _reportService.GetReports();
            return result.Select(r => r.ToModel());
        }
    }
}