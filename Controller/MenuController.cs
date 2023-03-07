using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using menu_upup.Config;
using menu_upup.Database;
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "register")] HttpRequest req,
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
            await context.SaveChangesAsync();
            return new OkObjectResult("Successfully registered user");
        }

        [FunctionName("Login")]
        public static async Task<IActionResult> Login(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string password = data?.password;
            await using var context = new MenuDatabase();
            var rows = context.UserDbSet.Where(x => x.Name == name && x.Password == password);
            if (rows.Any())
            {
                var user = rows.First();
                user.GenerateToken();
                context.Entry(user)
                    .Collection(x => x.Menus)
                    .Load();
                user.Menus.AsParallel().ForAll(menu => context.Entry(menu)
                    .Collection(x => x.Foods)
                    .Load());
                return new OkObjectResult(user);
            }
            return new BadRequestErrorMessageResult("This name has existed or password error");
        }

        [FunctionName("MenuPost")]
        public static async Task<IActionResult> MenuPost(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "menu")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var menus = JsonConvert.DeserializeObject<List<Menu>>(requestBody);
            await using var context = new MenuDatabase();
            context.MenuDbSet.AddRange(menus);
            await context.SaveChangesAsync();
            return new OkObjectResult("Successfully add menu");
        }

        [FunctionName("FoodPost")]
        public static async Task<IActionResult> FoodPost(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "food")] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var foods = JsonConvert.DeserializeObject<List<Food>>(requestBody);
                if (foods.Any(x => !Config.Config.FoodTypes.Contains(x.FoodType)))
                {
                    return new BadRequestErrorMessageResult($"The food type must in the follow list:{string.Join(",", Config.Config.FoodTypes.ToArray())}");
                }
                await using var context = new MenuDatabase();
                context.FoodDbSet.AddRange(foods);
                await context.SaveChangesAsync();
                return new OkObjectResult("Successfully add food");
            }
            catch (Exception e)
            {
                return new BadRequestErrorMessageResult($"{(e.InnerException ?? e).Message}");
            }
        }
    }
}
