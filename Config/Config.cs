using ConfigurationCenter.Config;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace ConfigurationCenter.Config
{
    internal class Config
    {

        public static string DatabaseConnectionString => Environment.GetEnvironmentVariable("DBServiceConnection");
    }
}