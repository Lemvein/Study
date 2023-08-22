using System;

namespace lab6_1
{
    class A
    {
        public A() { Console.WriteLine("Constructor A"); }
        public B b { set; get; }
        public int fa() { return 66; }
    }
    class B
    {
        public B() { Console.WriteLine("Constructor B"); }
        public A a { set; get; }
        public int fb() { return 666; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            B b = new B();

            b.a = a;
            a.b = b;

            Console.WriteLine("a.b.fb() = {0}", a.b.fb());
            Console.WriteLine("b.a.fa() = {0}", b.a.fa());
            Console.ReadKey();
        }
    }
}
