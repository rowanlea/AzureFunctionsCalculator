using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AzureFunctionsCalculator
{
    public static class Function1
    {
        [FunctionName("Add")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            int firstValue = 0;
            int secondValue = 0;

            if (req.Method == "GET")
            {
                string first = req.Query["first"];
                string second = req.Query["second"];

                int.TryParse(first, out firstValue);
                int.TryParse(second, out secondValue);
            }

            if (req.Method == "POST")
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                firstValue = data?.first;
                secondValue = data?.second;
            }

            return new OkObjectResult(firstValue + secondValue);
        }
    }
}
