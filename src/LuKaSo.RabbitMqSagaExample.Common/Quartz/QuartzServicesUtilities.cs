using Quartz;
using System;

namespace LuKaSo.RabbitMqSagaExample.Common.Quartz
{
    public static class QuartzServicesUtilities
    {
        /// <summary>
        /// Start job with time span repeat
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="scheduler"></param>
        /// <param name="runInterval"></param>
        public static void StartJob<TJob>(IScheduler scheduler, TimeSpan runInterval)
            where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder =>
                    scheduleBuilder
                        .WithInterval(runInterval)
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Start job with cron expression schedule
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <param name="scheduler"></param>
        /// <param name="cronExpression"></param>
        public static void StartJob<TJob>(IScheduler scheduler, string cronExpression)
            where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithCronSchedule(cronExpression, x => x.WithMisfireHandlingInstructionFireAndProceed())
                .WithSimpleSchedule(scheduleBuilder =>
                    scheduleBuilder
                        .WithIntervalInSeconds(1)
                        .WithRepeatCount(0))
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
