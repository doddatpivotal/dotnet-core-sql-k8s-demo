using System;
using System.Diagnostics;
using OpenTelemetry;
using Microsoft.Extensions.Configuration;


namespace employee_todo_list_api
{
    internal class ExtraTagProcessor : BaseProcessor<Activity>
    {

        public IConfiguration Configuration { get; }

        public ExtraTagProcessor(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override void OnStart(Activity activity)
        {
            activity.SetTag("application", Configuration["Management:Tracing:ApplicationName"]);
            activity.SetTag("cluster", Configuration["Management:Tracing:Cluster"]);
            activity.SetTag("shard", Environment.GetEnvironmentVariable("HOSTNAME"));
        }

    }
}