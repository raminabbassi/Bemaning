using System;
using System.Threading.Tasks;
using StaffingSolution.Services;

namespace StaffingSolutionTest.TestDoubles
{
    public class FakeEmailService : EmailService
    {
        public bool EmailSent { get; private set; }
        public string SentTo { get; private set; }

        public FakeEmailService() : base(null) { }

        public override async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            EmailSent = true;
            SentTo = email;
            await Task.CompletedTask;
        }
    }
}
