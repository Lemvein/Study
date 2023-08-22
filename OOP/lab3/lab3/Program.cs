using System;
using System.Threading;

namespace lab3
{
    class A
    {
        public A()
        {
            Console.WriteLine("Constructor A");
            this.varA = 1;
        }
        ~A() {
            Console.WriteLine("Dist A");
            Thread.Sleep(2000);
        }
        public virtual int F()
        {
            Console.WriteLine("Func class A");
            return this.varA + 20;
        }
        protected int varA { get; set; } //атрибут доступа
    }

    class B : A
    {
        public B()
        {
            Console.WriteLine("Constructor B");
            this.varA = 5;
        }
        ~B() {
            Console.WriteLine("Dist B");
            Thread.Sleep(2000);
        }
        public override int F() //замещение
        {
            Console.WriteLine("Func of class B");
            return this.varA + 40;
        }
    }

    class D : B
    {
        public D()
        {
            Console.WriteLine("Constructor D");
            this.varA = 50;
        }
        ~D() {
            Console.WriteLine("Dist D");
            Thread.Sleep(2000);
        }
        public override int F() //замещение
        {
            Console.WriteLine("Func of class D");
            return (this.varA + 100);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            A a = new A(); //а - полиморфная переменная
            Console.WriteLine("a.F() = {0}", a.F());
            Console.ReadKey();

            a = new B(); //подстановка
            Console.WriteLine("a.F() = {0}", a.F()); //замещение функции F()
            Console.ReadKey();

            a = new D(); //подстановка
            Console.WriteLine("a.F() = {0}", a.F()); //замещение функции F()
            Console.ReadKey();

            if (a.GetType() == typeof(B))
            {
                Console.WriteLine("a.GetType() == typeof(B)");
            }
            else 
            { 
                Console.WriteLine("a.GetType() !== typeof(B)");
                if (a.GetType() == typeof(D)) 
                {
                    Console.WriteLine("a.GetType() == typeof(D)");
                } 
                else 
                {
                    Console.WriteLine("a.GetType() !== typeof(D)");
                }
            
            }

            {
                B b = new B(); 
            } 
            Console.ReadKey();
            Thread.Sleep(2000);
        }
    }
}

