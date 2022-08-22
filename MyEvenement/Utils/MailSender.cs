using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyEvenement.Utils
{
    public class MailSender
    {
        private MailHolder mailHolder;
        private static readonly string _mail_id = "testsender420@outlook.com";
        private static readonly string _passeword = "TestTest@123";
        
        public MailSender(string to, string from, string subject, string body)
        {
            mailHolder = new MailHolder() { to = to, from = from, subject = subject, body = body };
        }

        public void send()
        {
            MailMessage message = new MailMessage(mailHolder.from, mailHolder.to);
            message.Subject=mailHolder.subject;
            message.Body = mailHolder.body;

            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(_mail_id, _passeword );

            client.EnableSsl = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void sendtest()
        {

            string to = "testsender420@yahoo.com"; //To address    
            string from = "testsender420@outlook.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.office365.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("testsender420@outlook.com", "TestTest@123");
            client.EnableSsl = true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }

    public class MailHolder{
        public string to { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
