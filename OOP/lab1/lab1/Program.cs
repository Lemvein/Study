using System;

namespace lab1
{
    class A
    {
        public A(B b, C c, J j)
        {
            Console.WriteLine("Constructor A");
            this.b = b;
            this.c = c;
            this.j = j;
            c.cq = 9;
        }
        public void mA()
        {
            Console.WriteLine("Method of A");
        }
        public B bA
        {
            set { Console.WriteLine("set b"); b = value; }
            get { Console.Write("get b -> "); return b; }
        }
        public C cA
        {
            set { Console.WriteLine("set c"); c = value; }
            get { Console.Write("get c -> "); return c; }
        }
        public J jA
        {
            set { Console.WriteLine("set j"); j = value; }
            get { Console.Write("get j -> "); return j; }
        }
        private B b = null;
        private C c = null;
        private J j = null;
    }
    class B
    {
        public B(D d)
        {
            Console.WriteLine("Constructor B");
            this.d = d;
        }
        public void mB()
        {
            Console.WriteLine("Method of B");
        }
        public D dA
        {
            set { Console.WriteLine("set d"); d = value; }
            get { Console.Write("get d -> "); return d; }
        }

        private D d = null;

    }
    class C
    {
        public C(E e, F f, K k)
        {
            Console.WriteLine("Constructor C");
            this.e = e;
            this.f = f;
            this.k = k;
        }
        public void mC()
        {
            Console.WriteLine("Method of C");
        }
        public E eA
        {
            set { Console.WriteLine("set e"); e = value; }
            get { Console.Write("get e -> "); return e; }
        }
        public F fA
        {
            set { Console.WriteLine("set f"); f = value; }
            get { Console.Write("get f -> "); return f; }
        }
        public K kA
        {
            set { Console.WriteLine("set f"); k = value; }
            get { Console.Write("get f -> "); return k; }
        }
        public int cq { get; set; }

        private E e = null;
        private F f = null;
        private K k = null;

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
            D d = new D();
            E e = new E();
            F f = new F();
            J j = new J();
            K k = new K();
            B b = new B(d);
            C c = new C(e, f, k);
            A a = new A(b, c, j);
            Console.WriteLine("Печать атрибута доступа:");
            Console.WriteLine(c.cq);
            Console.WriteLine("Передача по ссылке:");
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
