using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using System.Collections;
using System;
using System.Collections.Concurrent;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace menu_upup.Config
{
    public class Config
    {

        public static string DatabaseConnectionString => Environment.GetEnvironmentVariable("DBServiceConnection");
        public static HashSet<string> FoodTypes = new (){
            "主食","荤菜","素菜","凉菜","汤、粥","糕点小吃"
        };
    }
}