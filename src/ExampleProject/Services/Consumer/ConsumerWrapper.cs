namespace ExampleProject
{
    public class ConsumerWrapper
    {
        private readonly IReportService reportService;
        public ConsumerWrapper(IReportService reportService)
        {
            this.reportService= reportService;
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
                 channel.QueueDeclare(methodName, false, false, false, null);
               var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var reportArr = ea.Body.ToArray();
                    reportService.ApproveReports(reportArr);
                    Console.WriteLine("Received Message: {0}", "Report Approve Completed");
                };
                channel.BasicConsume("BasicTest", true, consumer);
                   response.Success= true;
                   response.Message=$"{MethodName} successfully add queue";
                } 
                catch (System.Exception ex)
                {
                     response.Success= true;
                   response.Message=$"{MethodName} can't add queue. Ex: {ex.Message}";
                }

            }

            return response;
        }
    }
}