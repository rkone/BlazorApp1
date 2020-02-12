using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var myClass = new MyClass();
            myClass.Run();
        }

        public class MyClass
        {
            private SampleContext _ctx;
            private CancellationTokenSource _tokenSource = new CancellationTokenSource();

            public void Run()
            {
                var i = 0;
                _ctx = new SampleContext();
                string input = null;
                while (input != "x")
                {
                    input = Console.ReadLine();
                    if (input == "s")
                    {
                        i++;
                        Task.Run(async () =>
                        {
                            Console.WriteLine($"Starting {i}");
                            _tokenSource.Cancel();
                            _tokenSource = new CancellationTokenSource();
                            var tokenSource = _tokenSource;
                            await _ctx.LongTask(tokenSource.Token);
                            if (!tokenSource.IsCancellationRequested)
                                Console.WriteLine($"Completed {i}");
                        });
                    }
                }
            }
        }
    }
}
