using System;

namespace lab8
{
    class A { }
    class B { }
    public interface IA
    {
        void F();
    }
    class D : IA
    {
        public void F() { Console.WriteLine("F() in class D"); }
    }
    class Swap
    {
        public void Fswap(ref A a, ref A b) 
        {
            A temp = a;
            a = b;
            b = temp;
        }
    }

    class SwapT<T>where T:class //конкретизация с ограничениями (T классом)
    {
        //ref позволяет передавать параметры, иначе - делается копия
        public void Fswap(ref T a, ref T b) //конкретизация параметров функции
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    class U<T> where T : class 
    {
        public U(T t) //конкретизация конструктора
        {
            if (t is IA) //конкретизация с ограничениями
            {
                Console.WriteLine("It is IA");
                this.t = t; //конктетизация атрибука
            }
            else Console.WriteLine("It is not IA");
        }
        public void f1() { Console.WriteLine("Hi"); }
        public T t { set; get; }
        public void F()
        {
            Console.WriteLine(t is IA);
            Console.WriteLine(t);
            if (t is IA)
            {
                IA ia = (IA)t;
                ia.F();
            }
            else Console.WriteLine("False");
        }
    }

    class L<T1, T2> where T1:class //множественная конкретизация
                    where T2:class
    {
        public void f1(T1 t1, T2 t2)
        {
            Console.WriteLine("Hi T1, T2");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Swap swap = new Swap();
            A a1, a2 = null;
            a1 = new A();
            a2 = new A();
            Console.WriteLine();

            Console.WriteLine("a1 addr: {0}", a1.GetHashCode().ToString());
            Console.WriteLine("a2 addr: {0}", a2.GetHashCode().ToString());
            Console.WriteLine();

            swap.Fswap(ref a1, ref a2);
            Console.WriteLine("a1 addr: {0}", a1.GetHashCode().ToString());
            Console.WriteLine("a2 addr: {0}", a2.GetHashCode().ToString());
            Console.WriteLine();

            SwapT<A> swapt = new SwapT<A>();
            swapt.Fswap(ref a1, ref a2);
            Console.WriteLine("a1 addr: {0}", a1.GetHashCode().ToString());
            Console.WriteLine("a2 addr: {0}", a2.GetHashCode().ToString());
            Console.WriteLine();

            U<A> ua = new U<A>(a1);
            ua.F();
            Console.WriteLine();

            U<D> ud = new U<D>(new D());
            ud.F();
            D d1 = new D();
            Console.WriteLine("d1 addr: {0}", d1.GetHashCode().ToString());
            U<D> ud1 = new U<D>(d1);
            ud1.F();
            Console.WriteLine();

            L<D,A> l= new L<D,A>();
            l.f1(d1,a1);
            Console.ReadKey();
        }
    }
}
