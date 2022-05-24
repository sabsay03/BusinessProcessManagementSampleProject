using Quartz;
using Quartz.Impl;

namespace OrganizationService
{   public static class SchedulerHelper
    {
        public static async void SchedulerSetup()
        {
            var _scheduler = await new StdSchedulerFactory().GetScheduler();
            await _scheduler.Start();

            var showDateTimeJob = JobBuilder.Create<CheckProjectStatusJob>()
                .WithIdentity("CheckProjectStatusJob")
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("CheckProjectStatusJob")
                .StartNow()
                .WithSimpleSchedule(builder => builder.WithIntervalInHours(24).RepeatForever()) //.WithCronSchedule("*/1 * * * *")
                .Build();

            var showDateTimeJob1 = JobBuilder.Create<CheckProjectLastDaysJob>()
             .WithIdentity("CheckProjectLastDaysJob")
             .Build();
            var trigger1 = TriggerBuilder.Create()
                .WithIdentity("CheckProjectLastDaysJob")
                .StartNow()
                .WithSimpleSchedule(builder => builder.WithIntervalInHours(24).RepeatForever()) //.WithCronSchedule("*/1 * * * *")
                .Build();

            var showDateTimeJob2 = JobBuilder.Create<CheckMissionLastDaysJob>()
            .WithIdentity("CheckMissionLastDaysJob")
            .Build();
            var trigger2 = TriggerBuilder.Create()
                .WithIdentity("CheckMissionLastDaysJob")
                .StartNow()
                .WithSimpleSchedule(builder => builder.WithIntervalInHours(24).RepeatForever()) //.WithCronSchedule("*/1 * * * *")
                .Build();


            var showDateTimeJob3 = JobBuilder.Create<CheckMissionStatusJob>()
           .WithIdentity("CheckMissionStatusJob")
           .Build();
            var trigger3 = TriggerBuilder.Create()
                .WithIdentity("CheckMissionStatusJob")
                .StartNow()
                .WithSimpleSchedule(builder => builder.WithIntervalInHours(24).RepeatForever()) //.WithCronSchedule("*/1 * * * *")
                .Build();


            var result = await _scheduler.ScheduleJob(showDateTimeJob, trigger);
            var result1 = await _scheduler.ScheduleJob(showDateTimeJob1, trigger1);
            var result2 = await _scheduler.ScheduleJob(showDateTimeJob2, trigger2);
            var result3 = await _scheduler.ScheduleJob(showDateTimeJob3, trigger3);
        }
    }
}
