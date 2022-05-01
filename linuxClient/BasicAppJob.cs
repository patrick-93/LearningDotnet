using Serilog;
using Quartz;

namespace DotnetClient
{
	public class BasicAppJob : IJob
	{
		public BasicAppJob() {}
		public async Task Execute(IJobExecutionContext ctx)
		{
			await Task.Run( () => {
				JobDataMap map = ctx.JobDetail.JobDataMap;
				string? name = map.GetString("name");
				Log.Information("job execution started");
				DriveInfo[] drives = DriveInfo.GetDrives();
				foreach (DriveInfo d in drives)
				{
					try
					{
						Log.Information("Format: " + d.DriveFormat);
						Log.Information("Type: " + d.DriveType);
						Log.Information("Label: " + d.VolumeLabel);
						Log.Information("");
					}
					catch (SchedulerException)
					{
						Log.Error("Hit a scheduler exception");
					}
					catch (UnauthorizedAccessException)
					{
						Log.Error($"Unauthorized acess exception to {d.ToString()}");
					}
				}
			});
		}
	}
}