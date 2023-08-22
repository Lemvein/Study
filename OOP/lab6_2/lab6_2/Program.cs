using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab6_2
{
    class D
    {
        public D()
        {
            this.c = new C[N];
            Console.WriteLine("Constructor D");
        }
        public D(int n)
        {
            this.N = n;
            this.c = new C[N];
            Console.WriteLine("Constructor D for N");
        }
        public void setC(C c)
        {
            if (size < N)
            {
                this.c[size] = c;
                size++;
            }
        }
        //метод, позволяющий просматривать последовательно объекты класса C, связанные с объектом класса D
        public C getNext(int index)
        {
            if (index < size)
            {
                return c[index];//возвращаем ссылку
            }
            return null;
        }

        public int size = 0;
        private C[] c = null;
        private int N = 5;
    }

    class C
    {
        public C()
        {
            Console.WriteLine("Constructor C");
        }
        public C(D d)
        {
            d.setC(this);
            Console.WriteLine("Constructor C with atribute");
        }
        public int j() { return v; }
        private int v = 10;
        public D d { set; get; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            D d1 = new D();
            D d2 = new D(10);
            Console.WriteLine();

            C c1 = new C();
            d1.setC(c1);
            C c2 = new C();
            d1.setC(c2);
            Console.WriteLine();

            c1.d = d1;
            Console.WriteLine("c1.d = {0}", c1.d);
            Console.WriteLine();

            Console.WriteLine("c1.d.getNext(1) = {0}", c1.d.getNext(1));
            Console.WriteLine("d1.getNext().j() = {0}", d1.getNext(1).j());
            Console.WriteLine("d1.getNext().j() = {0}", d1.getNext(1).j() + 1);
            Console.WriteLine();

            C c3 = new C(d1);

            Console.ReadKey();
        }
    }
}
