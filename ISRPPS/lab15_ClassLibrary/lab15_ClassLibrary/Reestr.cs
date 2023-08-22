using System;

namespace lab15_ClassLibrary
{
    public class Reestr
    {
        public string F { set; get; }
        public string I { set; get; }
        public int year { set; get; }

        public Reestr()
        {
            this.F = "I";
            this.I = "D";
            this.year = 88;
        }
    }
}
