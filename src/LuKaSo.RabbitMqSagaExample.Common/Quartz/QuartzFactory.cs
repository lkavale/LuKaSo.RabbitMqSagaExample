using Quartz;
using Quartz.Spi;
using System;

namespace LuKaSo.RabbitMqSagaExample.Common.Quartz
{
    public class QuartzFactory : IJobFactory
    {
        /// <summary>
        /// Service provider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public QuartzFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// New job
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_serviceProvider.GetService(bundle.JobDetail.JobType);
        }

        /// <summary>
        /// Return job dummy
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job) { }
    }
}
