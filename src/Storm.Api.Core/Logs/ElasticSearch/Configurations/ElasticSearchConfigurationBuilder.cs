using System;
using System.Collections.Generic;
using Storm.Api.Core.Logs.ElasticSearch.Senders;

namespace Storm.Api.Core.Logs.ElasticSearch.Configurations
{
	public interface IElasticSearchConfigurationBuilder
	{
		ElasticSearchConfiguration Build();
		IElasticSearchConfigurationBuilder WithBasicAuthentication(string username, string password);
		IElasticSearchConfigurationBuilder WithNode(string node);
		IElasticSearchConfigurationBuilder WithNodes(params string[] nodes);
		IElasticSearchConfigurationBuilder WithNodes(IEnumerable<string> nodes);
		IElasticSearchConfigurationBuilder WithMinimumLogLevel(LogLevel minimumLogLevel);
		IElasticSearchConfigurationBuilder WithQueueSender();
		IElasticSearchConfigurationBuilder WithImmediateSender();
		IElasticSearchConfigurationBuilder WithIndex(string index, string type);
	}

	internal class ElasticSearchConfigurationBuilder : IElasticSearchConfigurationBuilder
	{
		private ElasticSearchConfiguration _configuration = new ElasticSearchConfiguration();
		private ElasticSearchConfiguration Configuration => _configuration ?? throw new InvalidOperationException("You can not change parameters in builder after calling Build()");

		public ElasticSearchConfiguration Build()
		{
			ElasticSearchConfiguration configuration = Configuration;
			_configuration = null;
			return configuration;
		}

		public IElasticSearchConfigurationBuilder WithBasicAuthentication(string username, string password)
		{
			Configuration.UseBasicAuthentication(username, password);
			return this;
		}

		public IElasticSearchConfigurationBuilder WithNode(string node)
		{
			Configuration.AddNode(node);
			return this;
		}

		public IElasticSearchConfigurationBuilder WithNodes(params string[] nodes) => WithNodes((IEnumerable<string>)nodes);

		public IElasticSearchConfigurationBuilder WithNodes(IEnumerable<string> nodes)
		{
			Configuration.AddNodes(nodes);
			return this;
		}

		public IElasticSearchConfigurationBuilder WithMinimumLogLevel(LogLevel minimumLogLevel)
		{
			Configuration.MinimumLogLevel = minimumLogLevel;
			return this;
		}

		public IElasticSearchConfigurationBuilder WithQueueSender()
		{
			Configuration.UseSender((client, logService) => new BackOffQueueLogSender(logService, client));
			return this;
		}

		public IElasticSearchConfigurationBuilder WithImmediateSender()
		{
			Configuration.UseSender((client, logService) => new ImmediateQueueLogSender(logService, client));
			return this;
		}

		public IElasticSearchConfigurationBuilder WithIndex(string index, string type)
		{
			Configuration.UseIndex(index, type);
			return this;
		}
	}
}