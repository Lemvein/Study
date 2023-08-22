using System;

namespace lab2._1
{
    class A
    {
        public A()
        {
            Console.WriteLine("Constructor A");
            c.c1 = 9;
        }
        public void mA()
        {
            Console.WriteLine("Method of A");
        }
        public B bA
        {
            get { Console.Write("get b -> "); return b; }
        }
        public C cA
        {
            get { Console.Write("get c -> "); return c; }
        }
        public J jA
        {
            get { Console.Write("get j -> "); return j; }
        }
        private B b = new B();
        private C c = new C();
        private J j = new J();
    }
    class B
    {
        public B() {
            Console.WriteLine("Constructor B");
        }
        public void mB()
        {
            Console.WriteLine("Method of B");
        }
        public D dA
        {
            get { Console.Write("get d -> "); return d; }
        }

        private D d = new D();

    }
    class C
    {
        public C()
        {
            Console.WriteLine("Constructor C");
            this.c1 = 15;
        }
        public void mC()
        {
            Console.WriteLine("Method of C");
        }
        public E eA
        {
            get { Console.Write("get e -> "); return e; }
        }
        public F fA
        {
            get { Console.Write("get f -> "); return f; }
        }
        public K kA
        {
            get { Console.Write("get f -> "); return k; }
        }
        public int c1 { get; set; }

        private E e = new E();
        private F f = new F();
        private K k = new K();

    }
    class D
    {
        public D() {
            Console.WriteLine("Constructor D");
        }
        public void mD()
        {
            Console.WriteLine("Method of D");
        }
    }
    class E
    {
        public E() {
            Console.WriteLine("Constructor E");
        }
        public void mE()
        {
            Console.WriteLine("Method of E");
        }
    }
    class F
    {
        public F() {
            Console.WriteLine("Constructor F");
        }
        public void mF()
        {
            Console.WriteLine("Method of F");
        }
    }
    class J
    {
        public J() {
            Console.WriteLine("Constructor J");
        }
        public void mJ()
        {
            Console.WriteLine("Method of J");
        }
    }
    class K
    {
        public K() {
            Console.WriteLine("Constructor K");
        }
        public void mK()
        {
            Console.WriteLine("Method of K");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            a.mA();
            a.bA.mB();
            a.cA.mC();
            a.jA.mJ();
            a.bA.dA.mD();
            a.cA.eA.mE();
            a.cA.fA.mF();
            a.cA.kA.mK();
            Console.ReadKey();
        }
    }
}
