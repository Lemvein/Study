using System;

namespace lab_4
{
    class A     //супер-класс 
    {
        public A()
        {
            Console.WriteLine("Сonstructor A");
            this.a = 1;
        }
        ~A()
        {
            Console.WriteLine("Distructor ~A()");
            //Thread.Sleep(2000);
        }
        public virtual int fa()     //virtual для замещения функции  
        {
            Console.WriteLine("class A fa() ");
            return a + 1;
        }
        public int Aa
        {
            set { a = value; }
            get { Console.WriteLine("  get  "); return a; }
        }
        protected int a { set; get; }    //атрибут везде наследуется (атрибут доступа по умолчанию)
    }

    class B : A 
    {
        public B()
        {
            Console.WriteLine("Constructor B");
            this.a = 20;
            this.b = 10;
            this.b1 = -1;
        }
        ~B()
        {
            Console.WriteLine("distructor B");
            //Thread.Sleep(2000);
        }

        //замещение + выполнение предыдущего кода в суперклассе
        //расширение функции
        public override int fa()
        {
            Console.WriteLine("class B fa()");
            base.fa();
            return a + 10;
        }
        public int fb()
        {
            Console.WriteLine("class B fb()");
            return a + b + 10;
        }
        protected int b { set; get; }
        public int b1 { set; get; }
    }


    abstract class C
    {
        abstract public int fc_1();
        abstract public int fc_2();
        public void print() { Console.WriteLine("class C print"); }//будет просто наследоваться
        public int Cc
        {
            set { c = value; }
            get { Console.WriteLine("   get  "); return c; }
        }
        protected int c = 1;
    }


    class E : C
    {
        public E() { this.c = 22; }
        public override int fc_1() //замещаем эту функцию из класса С
        {
            Console.WriteLine("class E fc1()");
            return c * 5;
        }

        public override int fc_2() //замещаем эту функцию из класса С
        {
            Console.WriteLine("class E fc2()");
            return c / 2;
        }
    }

    class F : C
    {
        public F() { this.c = 1022; }
        public override int fc_1() //замещаем эту функцию из класса С
        {
            Console.WriteLine("class F fc1()");
            return c * 15;
        }

        public override int fc_2() //замещаем эту функцию из класса С
        {
            Console.WriteLine("class F fc2()");
            return c / 2 + 1000;
        }
    }

    interface J 
    {
        int fj_1(); 
        int fj_2();
        int fj() { return 0; }
    }

    class K : J
    {
        public K() { }
        public int fj_1() { return 10; }
        public int fj_2() { return 20; }
        public int a { get; set; }

    }

    class D : B
    {
        public D() 
        {
            Console.WriteLine("Constructor D");
            this.a = 200;
        }
        public override int fa()
        {
            Console.WriteLine("class D fa()");
            return a + 4;
        }
    }

    class V : D 
    {
        public V()
        {
            Console.WriteLine("Constructor V");
            base.fa(); }
        public override int fa() 
        {
            Console.WriteLine("class V fa()");
            return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            //создаём обьект
            Console.WriteLine("a.fa() = {0}", a.fa());

            a = new B();
            Console.WriteLine("a.fa() = {0}", a.fa());      //Замещённая функция (вместо супер-класса становится функцией подкласса)
            Console.WriteLine();

            Console.WriteLine("Expansion");
            Console.WriteLine("(B)a.fb() = {0}", ((B)a).fb());      //расширение по функциям
            Console.WriteLine("(B)a.b1 = {0}", ((B)a).b1);        //расширение по атрибутам

            Console.WriteLine("Specification (abstract class)");
            C c = null;     //создаём ссылку на обьект  и подставляем под него обьект который мы пронаследовали 
            c = new E();
            c.Cc = 455;     //поработали с обьектом суперкласса
            c.print();      //пронаследовалась
            Console.WriteLine("c.fc_1() = {0}", c.fc_1());      //замещение fc_1() класса С функцией fc() класса Е
            Console.WriteLine("c.fc_2() = {0}", c.fc_2());

            c = new F();        //подставляем обьект F
            c.print();      //пронаследовалась
            Console.WriteLine("c.fc_1() = {0}", c.fc_1());       //замещение fc_1() класса С функцией fc_1() класса F
            Console.WriteLine("c.fc_2() = {0}", c.fc_2());

            Console.WriteLine("Specification (interface)");
            J j = null;
            j = new K(); 
            Console.WriteLine(" j.fj_1() = {0}", j.fj_1());
            Console.WriteLine(" j.fj_2() = {0}", j.fj_2());

            Console.WriteLine("Specialization");
            a = new D();
            Console.WriteLine("a.fa() = {0}", a.fa());


            Console.WriteLine("Construction");
            a = new V();
            Console.WriteLine("a.fa() = {0}", a.fa());
            
            Console.ReadKey();
        }
    }
}