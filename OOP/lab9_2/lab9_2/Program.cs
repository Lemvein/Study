using System;
using System.Collections.Generic;

namespace lab9_2
{
    delegate int Lambda(int x, int y);
    delegate void Lambda_OP();
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Step 1: Lambda");
            //Lambda lambda = null;
            Lambda lambda = (x, y) => { return x + y; };
            Console.WriteLine("lambda = {0}", lambda(3, 7));
            Lambda_OP print = () => Console.WriteLine("Hello world"); //без параметров
            print();
            Console.WriteLine();

            Console.WriteLine("Step 2: Using Lambda");

            Func<int, int> square = (s) => s * s; //конкретизируемая функция
            Console.WriteLine("square(49) = {0}", square(49)); 
            Console.WriteLine();

            List<int> elements = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
            int Num = elements.Find(x => x % 2 != 0);
            Console.WriteLine("First nechet: {0}", Num);
            Console.WriteLine();

            int Num_List = elements.Find(x => (x >= 3 ) && (x <= 7));
            Console.WriteLine("Num_List[3;7]: {0}", Num_List);
            Console.ReadKey();
        }
    }
}
