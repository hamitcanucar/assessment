namespace ExampleProject
{
    public class ProducerWrapper
    {
        public GenericResponse<bool> PublishQueue(string methodName,string[] reportGuids)
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
                channel.BasicPublish("", methodName, null, reportGuids.ToString);
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