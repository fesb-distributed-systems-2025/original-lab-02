class Program
{
    private static decimal Person1_AccountBalance = 1000;
    private static decimal Person2_AccountBalance = 750;
    private static readonly object Person1_AccountLock = new object();
    private static readonly object Person2_AccountLock = new object();
    private static void DoWork1()
    {
        Console.WriteLine("[P1->P2] :: Transfering 100 Euros from P1 to P2.");

        Console.WriteLine("[P1->P2] :: Locking Account P1...");
        lock (Person1_AccountLock)
        {
            Console.WriteLine("[P1->P2] :: Account P1 locked!");

            Thread.Sleep(1000); // Simulate some work to provoke the deadlock!

            Console.WriteLine("[P1->P2] :: Locking Account P2...");
            lock (Person2_AccountLock)
            {
                Console.WriteLine("[P1->P2] :: Account P2 locked!");


                Console.WriteLine($"[P1->P2] :: Current balance: P1 = {Person1_AccountBalance} Euros, P2 = {Person2_AccountBalance}");
                Person1_AccountBalance -= 100;
                Person2_AccountBalance += 100;
                Console.WriteLine($"[P1->P2] :: Balance after transfering 100 Euros: P1 = {Person1_AccountBalance} Euros, P2 = {Person2_AccountBalance}");
                Console.WriteLine("[P1->P2] :: Unlocking Account P2...");
            }
            Console.WriteLine("[P1->P2] :: Account P2 unlocked!");

            Console.WriteLine("[P1->P2] :: Unlocking Account P1...");
        }
        Console.WriteLine("[P1->P2] :: Account P1 unlocked!");
    }

    private static void DoWork2()
    {
        Console.WriteLine("[P1<-P2] :: Transfering 100 Euros from P1 to P2.");

        Console.WriteLine("[P1<-P2] :: Locking Account P2...");
        lock (Person2_AccountLock)
        {
            Console.WriteLine("[P1<-P2] :: Account P2 locked!");

            Thread.Sleep(1500); // Simulate some work to provoke the deadlock!

            Console.WriteLine("[P1<-P2] :: Locking Account P1...");
            lock (Person1_AccountLock)
            {
                Console.WriteLine("[P1<-P2] :: Account P1 locked!");

                Console.WriteLine($"[P1<-P2] :: Current balance: P1 = {Person1_AccountBalance} Euros, P2 = {Person2_AccountBalance}");
                Person1_AccountBalance += 50;
                Person2_AccountBalance -= 50;
                Console.WriteLine($"[P1<-P2] :: Balance after transfering 50 Euros: P1 = {Person1_AccountBalance} Euros, P2 = {Person2_AccountBalance}");
                Console.WriteLine("[P1<-P2] :: Unlocking Account P1...");
            }
            Console.WriteLine("[P1<-P2] :: Account P1 unlocked!");

            Console.WriteLine("[P1<-P2] :: Unlocking Account P2...");
        }
        Console.WriteLine("[P1<-P2] :: Account P2 unlocked!");
    }

    public static async Task Main()
    {
        /*
         * 
         * The idea is to transfer 100 Euros from Person 1 to Person 2
         * and then transfer 50 Euros from Person 2 to Person 1.
         * 
         */

        // For testing purposes, let's first use only one thread
        Console.WriteLine("*******************************************");
        Console.WriteLine("One thread - sequentially");
        DoWork1();
        DoWork2();
        Console.WriteLine("*******************************************");


        // Now do both transacions at the same time in different threads
        Console.WriteLine("*******************************************");
        Console.WriteLine("Multiple thread - In parallel");
        var tasks = new Task[]
        {
            Task.Run(DoWork1),
            Task.Run(DoWork2)
        };

        await Task.WhenAll(tasks);
        Console.WriteLine("*******************************************");


    }
}