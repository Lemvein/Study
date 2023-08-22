using System;

namespace lab9_1
{
    //делегат 
    delegate int A(int x, int y);
    delegate void B();
    class Program
    {
        static void Main(string[] args)
        {
            B b = delegate () { Console.WriteLine("B b = delegate()"); };
            
            A a1 = new A(Add);
            A a = null;
            a = Add;  
            Console.WriteLine();

            int result = a(20, 10);
            Console.WriteLine("Add(25, 7) = {0}",result);
            b();
            Console.WriteLine();

            Console.WriteLine("a = delegate (int x, int y) { return x % y; }");
            a = delegate (int x, int y) { return x % y; };
            Console.WriteLine("a(25, 7) = {0}", a(25, 7));
            b();
            Console.WriteLine();

            Console.WriteLine("a = delegate (int x, int y) { return x * y; }");
            a = delegate (int x, int y) { return x * y; };
            Console.WriteLine("a(25, 7) = {0}", a(25, 7));
            Console.WriteLine();

            Console.WriteLine("Delegate as poiner");
            A apoiner = null;
            apoiner = Add;
            apoiner += Multiply;
            Console.WriteLine("result {0}", apoiner.Invoke(37, 5));
            Console.WriteLine();

            apoiner -= Multiply;
            Console.WriteLine("apoiner -= Multiply = {0}", apoiner.Invoke(37, 5));
            apoiner += Multiply;
            Console.WriteLine("apoiner += Multiply = {0}", apoiner.Invoke(37, 5));
            apoiner -= Add;
            Console.WriteLine("apoiner -= Add = {0}", apoiner.Invoke(37, 5));
            apoiner += delegate (int x, int y) { return x * y; };
            Console.WriteLine("apoiner += delegate = {0}", apoiner.Invoke(37, 6));
            Console.WriteLine();

            Console.WriteLine("Delegate as parametr:");
            Multicast(15, 45, apoiner);

            Console.ReadKey();

        }
        private static int Add(int x, int y)
        {
            return x + y;
        }
        private static int Multiply(int x, int y)
        {
            return x * y;
        }
        public static void Multicast(int x, int y, A apoiner)
        {
            apoiner.Invoke(x, y);
        }

    }
}
