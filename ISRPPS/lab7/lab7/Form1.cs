using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7
{
    public partial class Form1 : Form
    {
        int[] request = new int[3];
        ChainofResponsibility chr = new ChainofResponsibility();

        public Form1()
        {
            InitializeComponent();
        }
        public class ChainofResponsibility
        {
            Handler h1 = new H1();
            Handler h2 = new H2();
            Handler h3 = new H3();

            public ChainofResponsibility()
            {
                h1.successor = h2;
                h2.successor = h3;
                h3.successor = null;

            }

            public void run(int[] request)
            {
                foreach (int i in request)
                {
                    h1.HandlerRequest(i);
                }
            }

        }
        abstract class Handler
        {
            public Handler successor { set; get; } = null;

            public abstract void HandlerRequest(int action);
        }

        class H1 : Handler
        {
            public override void HandlerRequest(int action)
            {
                if (action == 1)
                    MessageBox.Show("1 request");
                else if (successor != null)
                    successor.HandlerRequest(action);
            }
        }
        class H2 : Handler
        {
            public override void HandlerRequest(int action)
            {
                if (action == 2)
                    MessageBox.Show("2 request");
                else if (successor != null)
                    successor.HandlerRequest(action);
            }
        }
        class H3 : Handler
        {
            public override void HandlerRequest(int action)
            {
                if (action == 3)
                    MessageBox.Show("3 request");
                else if (successor != null)
                    successor.HandlerRequest(action);
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { int i = 0; request[i] = 1; }
            else { int i = 0; request[i] = 0; }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { int i = 1; request[i] = 2; }
            else { int i = 1; request[i] = 0; }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) { int i = 2; request[i] = 3; }
            else { int i = 2; request[i] = 0; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((checkBox1.Checked)||(checkBox2.Checked)||(checkBox3.Checked))
             chr.run(request);
            else MessageBox.Show("Please, select a mark");
        }
    }


}
