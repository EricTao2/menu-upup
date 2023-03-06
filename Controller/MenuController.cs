using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using ConfigurationCenter.Config;
using menu_upup.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace menu_upup.Controller
{
    public static class MenuController
    {
        [FunctionName("Register")]
        public static async Task<IActionResult> Register(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string password = data?.password;
            var user = new User()
            {
                Name = name,
                Password = password
            };
            await using var context = new MenuDatabase();
            var rows = context.UserDbSet.Where(x => x.Name == name);
            if (rows.Any())
            {
                return new BadRequestErrorMessageResult("This name has existed");
            }
            context.Add(user);
            return new OkObjectResult("Successfully registered user");
        }
    }
}
