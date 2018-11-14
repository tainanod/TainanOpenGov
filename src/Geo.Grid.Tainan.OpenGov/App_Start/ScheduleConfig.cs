using Quartz;
using Quartz.Impl;

namespace Geo.Grid.Tainan.OpenGov.App_Start
{
    /// <summary>
    /// 排程管理
    /// </summary>
    public class ScheduleConfig
    {
        /// <summary>
        /// 排程
        /// </summary>
        public static void SetupTasks()
        {
            IScheduler task = StdSchedulerFactory.GetDefaultScheduler();
            task.Start();
         
            CreatSchedule<Tasks.SendMail>(task, "SendMail", 1);
            //CreatSchedule<Tasks.SaveDiscussMailAdmin>(task, "SaveDiscussMailAdmin", 0);
        }

        /// <summary>
        /// 建立排程
        /// </summary>
        /// <typeparam name="T">model</typeparam>
        /// <param name="task">工作</param>
        /// <param name="jobName">名稱</param>
        /// <param name="minutes">間隔</param>
        private static void CreatSchedule<T>(IScheduler task, string jobName, int minutes) where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(jobName + "_Job", "group1")
                .Build();
                        
            if (minutes == 0)
            {
                //每天下午16:30分觸發
                ITrigger trigger = TriggerBuilder.Create()
                          .WithIdentity(jobName + "_Trigger", "group1")
                          .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(16, 30))
                          .ForJob(jobName + "_Job", "group1")
                          .Build();
                task.ScheduleJob(job, trigger);
            }
            else
            {
                ITrigger trigger = TriggerBuilder.Create()
                           .WithIdentity(jobName + "_Trigger", "group1")
                           .StartNow()
                           .WithSimpleSchedule(x => x.WithIntervalInMinutes(minutes)
                           .RepeatForever())
                           .Build();
                task.ScheduleJob(job, trigger);
            }
        }
    }
}