using Serilog;
using Quartz;
using Quartz.Impl;

namespace DotnetClient
{
    public class LinuxClient
    {
        public async void Start()
        {
            Log.Information("Starting Linux client");

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            
            AddJobs(scheduler);
            await scheduler.Start();

            while (true)
            {
                Log.Information($"Main thread {Thread.GetDomainID()} sleeping");
                Thread.Sleep(60000);
            }
        }

        private async void AddJobs(IScheduler s)
        {
            IJobDetail job1 = JobBuilder.Create<BasicAppJob>()
                .WithIdentity("basicjob", "group1").Build();
            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("simpletrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(
                    x => x.WithIntervalInSeconds(10).RepeatForever()
                )
                .Build();
                await s.ScheduleJob(job1, trigger1);
        }
        
        private void PopulateData()
        {
            Log.Information("Starting secondary thread");
            using (var db = new AppDbContext())
            {
                for (int i = 0; i < 10000; i++)
                {
                    Data item = new Data {Text = "Some placeholder text"};
                    db.Add(item);
                    db.SaveChanges();
                }
            }
            Log.Information("Secondary thread finishing");
        }
    }
}