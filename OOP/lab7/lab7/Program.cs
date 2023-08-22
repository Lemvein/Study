using System;

namespace lab7
{
    class D //класс клиента
    {
        public D() { Console.WriteLine("Constructor D"); }
        public void fd(E e)
        {
            Console.WriteLine("Algorithm:");
            this.d = e.e + e.fe();
            Console.WriteLine("Es.fe = {0}", Es.fe()); //утилита
            Console.WriteLine("Es.fetry = {0}", Es.fetry()); //утилита
        }
        private int d { set; get; }
    }

    class E //класс сервера
    {
        public E() { Console.WriteLine("Constructor E"); }
        public int fe() { return 25; }
        public static int fs() { return 94; }
        public int e { set; get; }
    }

    class Es //класс утилиты
    {
        public static int fe() { return 10; }
        public static int fetry() { return 20; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            D d = new D(); //клиент
            E e = new E(); //сервер
            d.fd(e); // клиент обращается к ресурсам сервера
            Console.ReadKey();
        }
    }
}
