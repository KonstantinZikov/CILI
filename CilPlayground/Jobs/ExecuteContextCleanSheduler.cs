﻿using Quartz;
using Quartz.Impl;

namespace CilPlayground.Jobs
{
    public class ExecuteContextCleanSheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<ExecuteContextCleaner>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x            
                    .WithIntervalInHours(1)    
                    .RepeatForever())                   
                .Build();                        

            scheduler.ScheduleJob(job, trigger);       
        }
    }
}