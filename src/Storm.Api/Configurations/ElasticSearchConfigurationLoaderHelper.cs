using System;
using Microsoft.Extensions.Configuration;
using Storm.Api.Core.Logs;
using Storm.Api.Core.Logs.ElasticSearch.Configurations;

namespace Storm.Api.Configurations
{
	public static class ElasticSearchConfigurationLoaderHelper
	{
		public static IElasticSearchConfigurationBuilder FromConfiguration(this IElasticSearchConfigurationBuilder builder, IConfiguration configuration)
		{
			/* Keys : Host ; User ; Password ; LogLevel */

			builder = builder
				.WithNode(configuration["Host"])
				.WithBasicAuthentication(configuration["User"], configuration["Password"]);

			if (configuration.GetValue<string>("LogLevel", null) is { } minimumLogLevelString && Enum.TryParse<LogLevel>(minimumLogLevelString, out LogLevel minimumLogLevel))
			{
				builder = builder.WithMinimumLogLevel(minimumLogLevel);
			}

			return builder;
		}
	}
}