using System;
using System.Threading;
using System.Threading.Tasks;

namespace cancelapp
{
    class Program
    {
        static readonly ManualResetEvent waitEvent = new ManualResetEvent(false);

        static void Loop(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} tick");
                    Thread.Sleep(300);
                }
            }
            finally
            {
                waitEvent.Set();
            }
        }

        static void Main(string[] args)
        {
            Console.ReadLine();

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            Task.Run(() => Loop(cts.Token));
            waitEvent.WaitOne();
        }
    }
}
