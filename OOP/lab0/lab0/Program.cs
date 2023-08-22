using System;

namespace lab0
{
    class Reestr
    {
        public Reestr()
        {
            Console.WriteLine("This is constructor without parameters");
        }
        public Reestr(string f, int num, int old, int year)
        {
            this.f = f;
            this.num = num;
            this.old = old;
            this.year = year;
        }
        public int CurrentYear()
        {
            int CY = old + year;
            return CY;
        }
        public int CurrentYear(int N)
        {
            int CY = old + year;
            return CY + N;
        }
        public static int operator +(Reestr a, Reestr b)
        {
            return a.year + b.year;

        }
        public static int operator -(Reestr a, Reestr b)
        {
            return a.year - b.year;

        }
        public void print()
        {
            Console.Write("{0}\t", f);
            Console.Write("{0}\t", num);
            Console.Write("{0}\t", old);
            Console.Write("{0}\t", year);
            Console.WriteLine();
        }
        public void print(int num)
        {
            Console.WriteLine("Overloading operation 'print'");
            Console.WriteLine("Value of parameter: {0}", num);
        }
        string f { set; get; }
        int num, old, year;

    }
    class Program
    {
        static void Main()
        {
            Reestr a = new Reestr("Homski", 1, 1950, 55);
            Console.WriteLine("Reestr a was created");
            Reestr b = new Reestr("Ulman", 2, 1990, 15);
            Console.WriteLine("Reestr b was created");
            Reestr c = new Reestr("Lindmar", 3, 1970, 35);
            Console.WriteLine("Reestr c was created");
            Console.WriteLine();
            Console.WriteLine("Constructor with parameters and operation without parameters:");
            a.print();
            b.print();
            c.print();
            Reestr d = new Reestr();
            int D = 30;
            d.print(D);
            Console.WriteLine();
            Console.WriteLine("Function CurrentYear without parameters:");
            Console.WriteLine("Current year: {0}", a.CurrentYear());
            Console.WriteLine();
            Console.WriteLine("Overloading function CurrentYear:");
            Console.WriteLine("Current year + 30: {0}", a.CurrentYear(D));
            Console.WriteLine();
            Console.WriteLine("Operator with sum: {0}", a + b);
            Console.WriteLine("Operator overload: {0}", a - b);
            Console.ReadKey();
        }
    }
}