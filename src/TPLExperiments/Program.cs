using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPLExperiments
{

    // https://michaelscodingspot.com/c-job-queues-part-3-with-tpl-dataflow-and-failure-handling/
    public sealed class TPLDataflowQueue
    {
        private readonly ActionBlock<string> jobs;
        private readonly CancellationTokenSource tokenSource = new();

        public TPLDataflowQueue()
        {
            var options = new ExecutionDataflowBlockOptions
            {
                EnsureOrdered = false,
                MaxDegreeOfParallelism = 10,
                SingleProducerConstrained = false,
                CancellationToken = this.tokenSource.Token,
            };


            this.jobs = new ActionBlock<string>((job) =>
            {
                Console.WriteLine(job);
            },
            options);
        }

        public void Enqueue(string job)
        {
            this.jobs.Post(job);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var queue = new TPLDataflowQueue();

            //var tasks = new Task[100];
            for (var i = 0; i < 100; ++i)
            {
                var job = i.ToString();
                //tasks[i] = Task.Run(() => queue.Enqueue(job));
                queue.Enqueue(job);
            }

            //await Task.WhenAll(tasks);

            var wait = new SpinWait();
            while (true)
            {
                Console.WriteLine("quit to exit");
                wait.SpinOnce();
                var input = Console.ReadLine();
                if (input == "quit")
                {
                    break;
                }
            }

            Console.WriteLine("done");
        }
    }
}
