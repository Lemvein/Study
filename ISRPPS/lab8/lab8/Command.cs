using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    interface Command
    {
        void Execute();
        void UnExecute();
    }

    class ConcretCommand : Command
    {
        char @operator;
        int operand;
        Calculator calculator;
        // Constructor
        public ConcretCommand(Calculator calculator,
        char @operator, int operand)
        {
            this.calculator = calculator;
            this.@operator = @operator;
            this.operand = operand;
        }
        
        public char Operator
        {
            set { @operator = value; }
        }
        public int Operand
        {
            set { operand = value; }
        }
        
        //  +++ interface 
        public void Execute()
        {
            calculator.Operation(@operator, operand);
        }
        public void UnExecute()
        {
            calculator.Operation(Undo(@operator), operand);
        }
        // Private helper function : приватные вспомогательные функции
        private char Undo(char @operator)
        {
            char undo;
            switch (@operator)
            {
                case '+':
                    undo = '-';
                    break;
                case '-':
                    undo = '+';
                    break;
                case '*':
                    undo = '/';
                    break;
                case '/':
                    undo = '*';
                    break;
                default:
                    undo = ' ';
                    break;
            }
            return undo;
        }
    }

    class Calculator
    {
        public int curr { set; get; } = 0;   // левый operand
        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+':
                    curr += operand;
                    break;
                case '-':
                    curr -= operand;
                    break;
                case '*':
                    curr *= operand;
                    break;
                case '/':
                    curr /= operand;
                    break;
            }

            //MessageBox.Show(""+curr + " , " + @operator + " , " + operand);
            
        }
    }

    // "Invoker" : вызывающий
    class User
    {
        // Initializers
        private Calculator _calculator = new Calculator();
        private List<Command> _commands = new List<Command>();
        private int _current = 0;

        public int getResult() { return _calculator.curr; }
        public void Redo(int levels)
        {
            //Console.WriteLine("\n---- Redo {0} levels ", levels);
            // Делаем возврат операций
            for (int i = 0; i < levels; i++)
                if (_current < _commands.Count - 1)
                    _commands[_current++].Execute();
        }
        public void Undo(int levels)
        {
            // Console.WriteLine("\n---- Undo {0} levels ", levels);
            // Делаем отмену операций
            for (int i = 0; i < levels; i++)
                if (_current > 0)
                    _commands[--_current].UnExecute();
        }

        Command command = null;
        public void Compute(char @operator, int operand)
        {
            // Создаем команду операции и выполняем её
            command = new ConcretCommand(
            _calculator, @operator, operand);
            command.Execute();
            // Добавляем операцию к списку отмены
            _commands.Add(command);
            _current++;
        }
    }

}
