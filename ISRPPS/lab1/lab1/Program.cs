using System;

namespace lab1
{
    class Date
    {
        private int year = 0;
        private int month = 0;
        private int day = 0;
        public int Year_A
        {
            set
            {
                if (value < 1917 || value > 2020)
                    throw new ArgumentOutOfRangeException("Year is out of range");
                else
                    year = value;
            }
            get { return year; }
        }
        public int Month_A
        {
            set
            {
                if (value < 1 || value > 12)
                    throw new ArgumentOutOfRangeException("Month is out of range");
                else
                    month = value;
            }
            get { return month; }
        }
        public int Day_A
        {
            set
            {
                if (value < 1 || value > 31)
                    throw new ArgumentOutOfRangeException("Day is out of range");
                else
                    day = value;
            }
            get
            { return day; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Step 1: Types");
            Console.WriteLine("size byte: {0} to {1}", byte.MinValue, byte.MaxValue);
            Console.WriteLine("size int: {0} to {1}", int.MinValue, int.MaxValue);
            Console.WriteLine("size short: {0} to {1}", short.MinValue, short.MaxValue);
            Console.WriteLine("size long: {0} to {1}", long.MinValue, long.MaxValue);
            Console.WriteLine("size float: {0} to {1}", float.MinValue, float.MaxValue);
            Console.WriteLine("size double: {0} to {1}", double.MinValue, double.MaxValue);
            Console.WriteLine("size decimal: {0} to {1}", decimal.MinValue, decimal.MaxValue);
            Console.WriteLine();

            Console.WriteLine("Step 2: Massive");
            float[] arr = new float[3] { 2.7f, 5.1f, 8.2f };
            foreach (float f in arr)
                Console.WriteLine("{0} ", f);
            Console.WriteLine();

            Console.WriteLine("Step 3: Date");
            Date date = new Date();
            for (; ; )
            {
                Console.WriteLine("Input year:");
                try
                {
                    date.Year_A = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (ArgumentOutOfRangeException) { Console.WriteLine("Out of Range, try again"); }
                catch (FormatException) { Console.WriteLine("Wrong format"); }

            }
            for (; ; )
            {
                Console.WriteLine("Input month:");
                try
                {
                    date.Month_A = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (ArgumentOutOfRangeException) { Console.WriteLine("Out of Range, try again"); }
                catch (FormatException) { Console.WriteLine("Wrong format"); }

            }
            for (; ; )
            {
                Console.WriteLine("Input day:");
                try
                {
                    date.Day_A = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (ArgumentOutOfRangeException) { Console.WriteLine("Out of Range, try again"); }
                catch (FormatException) { Console.WriteLine("Wrong format"); }

            }
            Console.WriteLine("Date: {0}.{1}.{2}", date.Day_A, date.Month_A, date.Year_A);
            Console.ReadKey();
        }
    }
}
