using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Web.Http.Description;
using Steeltoe.Common.Diagnostics;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Management.Endpoint;
using Steeltoe.Management.Endpoint.Health.Contributor;

namespace Steeltoe_CloudFoundry_Template1
{
	public class ManagementConfig
	{
		//public static IMetricsExporter MetricsExporter { get; set; }
		public static void ConfigureManagementActuators(IConfiguration configuration, ILoggerProvider dynamicLogger, IApiExplorer apiExplorer, ILoggerFactory loggerFactory = null)
		{
			ActuatorConfigurator.UseCloudFoundryActuators(configuration, dynamicLogger, GetHealthContributors(configuration), apiExplorer, loggerFactory);
		}

		public static void Start()
		{
			DiagnosticsManager.Instance.Start();
		}

		public static void Stop()
		{
			DiagnosticsManager.Instance.Stop();
		}

		private static IEnumerable<IHealthContributor> GetHealthContributors(IConfiguration configuration)
		{
			var healthContributors = new List<IHealthContributor>
						{
								new DiskSpaceContributor()
						};

			return healthContributors;
		}
	}
}