using System;

namespace lab2._2
{
    class A
    {
        public A()
        {
            Console.WriteLine("Constructor A");
            c.c1 = 9;
        }
        public class B
        {
            public B() {
                Console.WriteLine("Constructor B");
            }
            public class D
            {
                public D() {
                    Console.WriteLine("Constructor D");
                }
                public void mD()
                {
                    Console.WriteLine("method of D");
                }
            }
            public void mB()
            {
                Console.WriteLine("method of B");
            }
            public D dA
            {
                get { Console.Write("get d -> "); return d; }
            }
            private D d = new D();
        }
        public class C
        {
            public C()
            {
                Console.WriteLine("Constructor C");
                this.c1 = 10;
            }
            public class E
            {
                public E() {
                    Console.WriteLine("Constructor E");
                }
                public void mE()
                {
                    Console.WriteLine("method of E");
                }
            }
            public class F
            {
                public F() {
                    Console.WriteLine("Constructor F");
                }
                public void mF()
                {
                    Console.WriteLine("method of F");
                }
            }
            public class K
            {
                public K() {
                    Console.WriteLine("Constructor K");
                }
                public void mK()
                {
                    Console.WriteLine("method of K");
                }
            }
            public void mC()
            {
                Console.WriteLine("method of C");
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
                get { Console.Write("get k -> "); return k; }
            }
            private E e = new E();
            private F f = new F();
            private K k = new K();
            public int c1 { set; get; }
        }
        public class J
        {
            public J() {
                Console.WriteLine("Constructor J");
            }
            public void mJ() { Console.WriteLine("method of J"); }
        }
        public void mA()
        {
            Console.WriteLine("method of A");
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
        public void M()
        {
            c.kA.mK();
        }

        private B b = new B();
        private C c = new C();
        private J j = new J();
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
            a.M();
            Console.ReadKey();
        }
    }

}
