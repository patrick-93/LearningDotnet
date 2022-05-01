using System.Runtime.InteropServices;
using Serilog;

namespace DotnetClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupLogger();
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                    LinuxClient client = new LinuxClient();
                    client.Start();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                    WinClient client = new WinClient();
                    client.Start();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        static void SetupLogger()
        {
            string template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} "
                + "[{Level: u3}] {Message: lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: template
                )
                .WriteTo.File(
                    "logs/app.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    rollOnFileSizeLimit: true,
                    outputTemplate: template

                )
                .CreateLogger();
        }
    }

    
}