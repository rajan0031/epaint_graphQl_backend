
using System.Net;
using System.Net.Mail;
using DotNetEnv;

namespace MyGraphqlApp.Utils
{
    public class EmailVerification
    {
        public string GetOtp()
        {

            var random = new Random();

            var otp = "";

            for (int i = 0; i < 6; i++)
            {
                otp = otp + random.Next(10);
            }

            return otp;
        }

        public void SendOtpEmail(string toEmail, string otp)
        {

            try
            {
                Env.Load();
                var fromEmail = Environment.GetEnvironmentVariable("fromEmail");
                var appPassword = Environment.GetEnvironmentVariable("appPassword");
                Console.WriteLine("the email and pass is " + fromEmail + " " + appPassword);
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(fromEmail, appPassword),
                    EnableSsl = true,
                    Timeout = 20000,
                    DeliveryMethod = SmtpDeliveryMethod.Network

                };

                var mail = new MailMessage(fromEmail!, toEmail)
                {
                    Subject = "Dear user otp for the registration at Epaint",
                    Body = $"Your otp is : {otp}"
                };

                client.Send(mail);

                Console.WriteLine("Email sents successfully");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error sending mail {ex.Message}");
                throw;
            }
        }
    }
}