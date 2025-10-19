public class Program
{
    private static readonly int NumberOfTasks = 10;
    private static readonly int NumberOfIterations = 10;

    private static readonly Random random = new Random();

    // Number of threads allowed to execute some block of code at the same time
    private const int NumberOfThreadsAllowed = 3;
    // Semaphore - A lock that allows multiple (`NumberOfThreadsAllowed`) at the same time, but no more!s
    private static Semaphore Semaphore = new Semaphore(NumberOfThreadsAllowed, NumberOfThreadsAllowed);

    private static void DoCalculation()
    {
        for (int i = 0; i < NumberOfIterations; i++)
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is waiting for a free spot to upload a video...");

            // Take a lock (decrement semaphore by 1)
            Semaphore.WaitOne();

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} took a free spot and is uploading a video...");
            // Simulate video uploading work that lasts random X milliseconds
            // (between [500 and 4000] milliseconds)
            Thread.Sleep(500 + random.Next(4000));
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished uploading a video.");

            // Release the lock (increment semaphore by 1)
            Semaphore.Release();

            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} released a spot.");
        }
    }
    public static async Task Main()
    {
        Console.WriteLine($"Created a semphore that will allow {NumberOfThreadsAllowed} users to simultanously upload a video to our social network site.");
        Console.WriteLine($"Accepting users...");
        Thread.Sleep(1000);

        List<Task> tasks = new List<Task>();
        for (int i = 0; i < NumberOfTasks; i++)
        {
            var task = Task.Run(DoCalculation);
            tasks.Add(task);

            // Simulate next user after random X milliseconds
            // (between [1000 and 2000] milliseconds)
            Thread.Sleep(1000 + random.Next(1000));
        }
        await Task.WhenAll(tasks);
    }
}