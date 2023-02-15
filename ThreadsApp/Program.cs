using System.ComponentModel;
using System.Text;
using System.Threading;

namespace ThreadsApp
{
    internal class Program
    {
        
        static ParameterizedThreadStart parameterizedThreadStartStream = new ParameterizedThreadStart(SaveDataToFile);

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
        //Task 4-5 main delegate
        static void TaskDelegate(object massive)
        {
            ParameterizedThreadStart maxelemstart = new ParameterizedThreadStart(MaxElem);
            ParameterizedThreadStart minelemstart = new ParameterizedThreadStart(MinElem);
            ParameterizedThreadStart averageelemstart = new ParameterizedThreadStart(AverageInMassive);
            Thread thread1 = new Thread(maxelemstart);
            Thread thread2 = new Thread(minelemstart);
            Thread thread3 = new Thread(averageelemstart); 
            thread1.Start(massive);
            thread1.Join();
            thread2.Start(massive);
            thread2.Join();
            thread3.Start(massive);
            thread3.Join();
        }
        //Task 4-5 child delegate
        static void MaxElem(object massive)
        {
            int result = ((int[])massive).Max();
            Console.WriteLine(result.ToString());
            Thread thread = new Thread(parameterizedThreadStartStream);
            thread.Start(result);
        }
        static void MinElem(object massive)
        {
            int result = ((int[])massive).Min();
            Console.WriteLine(result.ToString());
            Thread thread = new Thread(parameterizedThreadStartStream);
            thread.Start(result);
        }
        static void AverageInMassive(object massive)
        {
            double result = ((int[])massive).Average();
            Console.WriteLine(result.ToString());
            Thread thread = new Thread(parameterizedThreadStartStream);
            thread.Start(result);
        }
        static void SaveDataToFile(object number)
        {
            FileStreamOptions options = new FileStreamOptions();
            options.Access = FileAccess.Write;
            options.Share = FileShare.Write;
            options.Mode = FileMode.Append;

            StreamWriter writer = new StreamWriter("task5.txt", Encoding.UTF8, options);            
            writer.WriteLine(number.ToString());
            writer.Close();
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