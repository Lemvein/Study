using System;

namespace lab9_3
{
    public class Message : EventArgs
    { 
        public string message { set; get; }
        public Message(string message) : base() { this.message = message; }
    }
    public class Publisher
    {
        public delegate void PublisherEventHandler(Message message);
        public delegate void EventHandler(Object sender, EventArgs args);
        public event PublisherEventHandler Changed;
        public void Ewraping(PublisherEventHandler Change) 
        { 
            Changed(new Message("Ewraping")); 
        }
        public Publisher() { }
        public void EventForPublisher(Message message)
        {
            Console.WriteLine("Event for all subscribers {0}", message.message);
            Changed(message);     
        }

    }
    public class Subscribers
    {
        int QRC { set; get; }
        public Subscribers(int QRC){ this.QRC = QRC; }
        public void subscribe(Message message) { Console.WriteLine("subscribe = {0}, {1}", this.QRC, message.message); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Step 1: Create");
            Publisher publisher = new Publisher();
            Subscribers Subscriber1 = new Subscribers(1);
            Subscribers Subscriber2 = new Subscribers(2);
            Subscribers Subscribern = new Subscribers(521);
            Console.WriteLine("Step 2: Sub");
            publisher.Changed += Subscriber1.subscribe;
            publisher.Changed += Subscriber2.subscribe;
            publisher.Changed += Subscribern.subscribe;
            Console.WriteLine("Step 3: Event");
            publisher.EventForPublisher(new Message("New book is ready!"));
            Console.ReadKey();
        }
    }
}
