using System;
using System.Text;
using ExampleProject.Models.ControllerModels;
using ExampleProject.Services.Abstract;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExampleProject
{
    public class ConsumerWrapper
    {
        private readonly IReportService reportService;
        public ConsumerWrapper(IReportService reportService)
        {
            this.reportService = reportService;
        }
        public GenericResponse<bool> ReceiverQueue<T>()
        {
            GenericResponse<bool> response = new GenericResponse<bool>();
            var factory = new ConnectionFactory() { HostName = "localhost", };
            //Create Connection 
            using (var connection = factory.CreateConnection())
            //Create Channel
            using (var channel = connection.CreateModel())
            {
                try
                {
                    channel.QueueDeclare("Report Receiver", false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var reportArr = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(reportArr);
                        reportService.ApproveReports(message.Split(','));
                        Console.WriteLine("Received Message: {0}", "Report Approve Completed");
                    };

                    channel.BasicConsume("Report Receiver", true, consumer);
                    response.Success = true;
                    response.Message = $"Report Receiver successfully add queue";
                }
                catch (System.Exception ex)
                {
                    response.Success = true;
                    response.Message = $"Report Receiver can't add queue. Ex: {ex.Message}";
                }

            }

            return response;
        }
    }
}