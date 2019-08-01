using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Program
    {        
        public static int[] arrayMedian;
        public static Stopwatch stopwatch = new Stopwatch();
        public static Stopwatch stopwatch2 = new Stopwatch();
        public static int basicOperationCounter;
        public static int basicOperationCounter2;
        public static double timeAvg1 = 0;
        public static double timeAvg2 = 0;
        public static int medianBruteForce;
        public static int median;
        

        static void Main(string[] args)
        {           
            for (int i = 10; i <= 10; i++) {
                arrayMedian = CreateArray(i, (i*2));
                
                for (int j =10; j <= 10; j++) {
                    basicOperationCounter = 0;                   
                    stopwatch.Start();
                    medianBruteForce = BruteForceMedian(arrayMedian);
                    stopwatch.Stop();
                    File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\BruteForceMedianMedianTimes.txt", stopwatch.Elapsed.TotalMilliseconds + Environment.NewLine);                   
                    timeAvg1 += stopwatch.Elapsed.TotalMilliseconds;
                    stopwatch.Reset();

                    basicOperationCounter2 = 0;
                    stopwatch2.Start();
                    median = Median(arrayMedian);                    
                    stopwatch2.Stop();
                    timeAvg2 += stopwatch2.Elapsed.TotalMilliseconds;
                    stopwatch2.Reset();
                    File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\MedianTimes.txt", stopwatch2.Elapsed.TotalMilliseconds + Environment.NewLine);
                }
               // File.AppendAllText(@"E:\\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\BruteForceMedianMedianTimes.txt", Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\BruteForceMedianMedianAnswer.txt", Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\BruteForceMedianMedianBasicOperations.txt", Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\MedianTimes.txt", Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\MedianAnswer.txt", Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\MedianBasicOperations.txt", Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\ArrayDisplay.txt", Environment.NewLine);
                File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\AvgFrom100BruteForceMedianMedianTimes.txt", (timeAvg1 / 100) + Environment.NewLine);
                File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\AvgFrom100MedianTimes.txt", (timeAvg2 / 100) + Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\MedianAnswer.txt", median + Environment.NewLine);
                File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\MedianBasicOperations.txt", basicOperationCounter2 + Environment.NewLine);
                //File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\BruteForceMedianMedianAnswer.txt", medianBruteForce + Environment.NewLine);
                File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\BruteForceMedianMedianBasicOperations.txt", basicOperationCounter + Environment.NewLine);
            }

            Console.WriteLine("Done");
            Console.ReadKey();

        }

        public static int[] CreateArray(int length, int randLength)
        {
            arrayMedian = new int[length];
            Random randNum = new Random();
            for (int i = 0; i < arrayMedian.Length; i++)
            {
                arrayMedian[i] = randNum.Next(1, randLength);
            }
            File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\ArrayDisplay.txt", "Array Length " + arrayMedian.Length + ": ");
            for (int i = 0; i < arrayMedian.Length; i++)
            {
                File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\ArrayDisplay.txt", arrayMedian[i].ToString() + ", ");
            }
            File.AppendAllText(@"E:\n8306699\CAB301\ASSIGNMENT 2 OUTPUT\ArrayDisplay.txt", Environment.NewLine);
            return arrayMedian;
        }

        public static int BruteForceMedian(int[] A)
        {            
            int k = (int)Math.Round((double)A.Length/2, MidpointRounding.AwayFromZero);
            int median = A[0];
            for (int i = 0; i < A.Length; i++)
            {          
                int numsmaller = 0;
                int numequal = 0;
                for (int j = 0; j < A.Length; j++)
                {
                    basicOperationCounter++;
                    if (A[j] < A[i])
                    {                        
                        numsmaller++;
                    }
                    else
                    {
                        if (A[j] == A[i])
                        {
                            numequal++;
                        }
                    }
                }
                if (numsmaller < k && k <= (numsmaller + numequal))
                {
                    median = A[i];
                }
            }

            return median;
        }

        public static int Median(int[] A)
        {
            if (A.Length == 1)
            {
                return A[0];
            }
            else
            {
                return Select(A, 0, A.Length / 2, A.Length - 1);
            }
        }

        public static int Select(int[] A, int l, int m, int h)
        {
            
            int pos = Partition(A, l, h);
            if (pos == m)
            {
                return A[pos];
            }
            else if (pos > m)
            {
                return Select(A, l, m, pos - 1);
            }
            else
            {
                return Select(A, pos + 1, m, h);
            }
        }

        public static int Partition(int[] A, int l, int h)
        {
            int pivotval = A[l];
            int pivotloc = l;
            for (int j = l + 1; j <= h; j++)
            {
                basicOperationCounter2++;
                if (A[j] < pivotval)
                {
                    pivotloc++;
                    swap(ref A[pivotloc], ref A[j]);
                }
            }
            swap(ref A[l], ref A[pivotloc]);
            return pivotloc;
        }

        public static void swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}