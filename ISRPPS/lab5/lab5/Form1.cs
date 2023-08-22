using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace lab5
{
    public partial class Form1 : Form
    {
        private static Form1 f1 = null;     //step2

        AbstractFactory bank = null;
        Client client = null;
        TestClass testclass = null;

        private Form1()     //step 1: Скрытый конструктор
        {
            InitializeComponent();
            bank = new BankSber();
            testclass = Singleton<TestClass>.CreatorInstance;
        }

        public static Form1 fA 
        { 
            get 
            {
                if (f1 == null)
                    f1 = new Form1();
                return f1;  
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.client = new Client(new BankSber());
            this.client.Run();
            this.label1.Text = this.client.abstractProductA.account.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(fA.testclass.TestProc());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.client = new Client(new BankVTB());
            this.client.Run();
            this.label2.Text = this.client.abstractProductB.account.ToString();
        }
    }   //end Form1

    public class Singleton<T> where T : class
    {
        protected Singleton(){ MessageBox.Show("Constructor Singleton<T>"); }

        private sealed class SingletonCreator<S> where S : class
        {
            public static S Instance { get; } = (S)typeof(S).GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new Type[0],
                new ParameterModifier[0]).Invoke(null); 
        }   //end class SingletonCreator
        public static T CreatorInstance { get { return SingletonCreator<T>.Instance; } }
    }   //end Singleton<T>

    public class TestClass : Singleton<TestClass>
    {
        private TestClass() { MessageBox.Show("Constructor TestClass"); }
        public string TestProc() { return "Hello World!"; } //надо вызвать

    }

    // ************************AbstractFactory****************************
    
    abstract class AbstractFactory  // factory1, factory2, ...
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();

    }

    abstract class AbstractProductA 
    {
        public int account { set; get; } = 222;
    }
    abstract class AbstractProductB 
    {
        public int account { set; get; } = 555;
        public abstract void Interact(AbstractProductA a);
    }

    class BankSber : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new CardA();
        }
        public override AbstractProductB CreateProductB()
        {
            return new CardB();
        }
    }

    class CardA : AbstractProductA 
    {
        //int account = 111;
    }
    class CardB : AbstractProductB 
    {
        public override void Interact(AbstractProductA a)
        {
            MessageBox.Show(this.GetType().Name + "   interact with   " + a.GetType().Name);
        }
        //int account = 120;
    }

    class BankVTB : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new CardVTB();
        }
        public override AbstractProductB CreateProductB()
        {
            return new CardVTBB();
        }
    }
    class CardVTB : AbstractProductA 
    {
        //int account = 222;
    }
    class CardVTBB : AbstractProductB 
    {
        public override void Interact(AbstractProductA a) 
        { 
            MessageBox.Show(this.GetType().Name + "  interact with  " + a.GetType().Name); 
        }
        //int account = 200;
    }



    class Client
    {
        public AbstractProductA abstractProductA;
        public AbstractProductB abstractProductB;

        public Client(AbstractFactory factory)
        {
            abstractProductA = factory.CreateProductA();
            abstractProductB = factory.CreateProductB();
        }

        public void Run() 
        {
            abstractProductB.Interact(abstractProductA);
        }
    }

}
