using System;
using StaffingSolution.Interfaces;

namespace StaffingSolution.Observers
{
    public class EmailService : IObserver
    {
        public void Update(string message)
        {
            Console.WriteLine($"Email sent: {message}");
        }
    }
}
