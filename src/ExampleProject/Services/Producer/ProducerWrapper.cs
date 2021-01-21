using System.Text;
using ExampleProject.Models.ControllerModels;
using RabbitMQ.Client;

namespace ExampleProject
{
    public class ProducerWrapper
    {
        public GenericResponse<bool> PublishQueue(string methodName, string[] reportGuids)
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
                      var body = Encoding.UTF8.GetBytes(reportGuids.ToString());  
                    channel.BasicPublish("Report Queue", methodName, null,body);
                    response.Success = true;
                    response.Message = $"{methodName} successfully add queue";
                }
                catch (System.Exception ex)
                {
                    response.Success = true;
                    response.Message = $"{methodName} can't add queue. Ex: {ex.Message}";
                }

            }

            return response;
        }
    }
}