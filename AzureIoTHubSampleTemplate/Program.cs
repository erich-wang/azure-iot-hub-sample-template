using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                tokenSource.Cancel();
                Console.WriteLine("Existing ...");
            };
            Console.WriteLine("Press CTRL+C to exit");

            Task.WaitAll(
                AzureIoTHub.SendDeviceToCloudMessageAsync(tokenSource.Token),
                AzureIoTHub.ReceiveMessagesFromDeviceAsync(tokenSource.Token)
                );
        }
    }
}
