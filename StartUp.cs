using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using menu_upup.Entity;
using MetricsParser;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Newtonsoft.Json;

[assembly: WebJobsStartup(typeof(StartUp))]

namespace MetricsParser;

public class StartUp : IWebJobsStartup
{
    public static string DataDirectoryPath => Path.GetFullPath(
                                                  Path.Combine(
                                                      Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                                          .Location) ?? string.Empty, "..")) +
                                              "/Database/Data";


    public void Configure(IWebJobsBuilder builder)
    {
    }
}