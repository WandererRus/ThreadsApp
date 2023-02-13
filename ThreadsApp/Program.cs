using System.Threading;

namespace ThreadsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task 1-3
            /*MyObject myObject= new MyObject(Int32.Parse(Console.ReadLine()));
            myObject.Start = Int32.Parse(Console.ReadLine()); 
            myObject.End = Int32.Parse(Console.ReadLine());
            //ThreadStart threadStart = new ThreadStart(ConsoleWrite);
            ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(ConsoleWrite);
            //Thread thread = new Thread(parameterizedThreadStart);
            //thread.IsBackground = true;
            //thread.Priority = ThreadPriority.Lowest;
            //thread.Start(myObject);
            for (int i = 0; i < myObject.threads.Length; i++) 
            {
                myObject.threads[i] = new Thread(parameterizedThreadStart);
            }
            for (int i = 0; i < myObject.threads.Length; i++)
            {
                myObject.threads[i].Start(myObject);
                myObject.threads[i].Join();
            }*/

            //Task4
            Random random = new Random();
            int[] massive = new int[10000];
            for (int i = 0; i < 10000; i++)
            {
                massive[i] = random.Next(15000);
            }
            ParameterizedThreadStart alltasks = new ParameterizedThreadStart(TaskDelegate);
            Thread threadall = new Thread(alltasks);
            threadall.Start(massive);
            threadall.Join();
        }
        //Task 1-3 delegate
        static void ConsoleWrite(object my)
        {
            int start = ((MyObject)my).Start;
            int end = ((MyObject)my).End;
            string message = ((MyObject)my).Message;
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine("Из потока: " + i);
            }
            Console.WriteLine(message);
        }
        //Task 4 main delegate
        static void TaskDelegate(object massive)
        {
            ParameterizedThreadStart maxelemstart = new ParameterizedThreadStart(MaxElem);
            ParameterizedThreadStart minelemstart = new ParameterizedThreadStart(MinElem);
            ParameterizedThreadStart averageelemstart = new ParameterizedThreadStart(AverageInMassive);
            Thread thread1 = new Thread(maxelemstart);
            Thread thread2 = new Thread(minelemstart);
            Thread thread3 = new Thread(averageelemstart);
            thread1.Start(massive);
            thread2.Start(massive);
            thread3.Start(massive);
        }
        //Task 4 child delegate
        static void MaxElem(object massive)
        {            
            Console.WriteLine(((int[])massive).Max().ToString());
            Console.WriteLine("Это читерство!");
        }
        static void MinElem(object massive)
        {
            Console.WriteLine(((int[])massive).Min().ToString());
        }
        static void AverageInMassive(object massive)
        {
            Console.WriteLine(((int[])massive).Average().ToString());
        }
    }
    //Task 1-3 class as object for task 1-3 delegate
    class MyObject 
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Message { get; set; } = "Это читерство";

        public Thread[] threads;

        public MyObject(int countThread)
        {
            if (countThread > 0)
            {
                threads = new Thread[countThread];
            }
            else
            {
                threads = new Thread[1];
            }
        }
    }
}