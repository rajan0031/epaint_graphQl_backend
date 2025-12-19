
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


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
                    DeliveryMethod = SmtpDeliveryMethod.Network

                };

                // start of the email sending in professional way 
                // var mail = new MailMessage(fromEmail!, toEmail)
                // {
                //     Subject = "Epaint Verification Code – Secure Your Registration",
                //     Body = $"Welcome to Epaint! 🎨\n\nYour one-time password (OTP) for completing registration is: {otp}\n\nEpaint connects you with trusted contractors to make painting your home simple and hassle-free.\n\nPlease use this code to verify your account and start exploring our network of professionals."
                // };

                var mail = new MailMessage(fromEmail!, toEmail) { Subject = "Epaint Verification Code – Secure Your Registration", IsBodyHtml = true };
                // Define the HTML body with inline logo reference 
                var verifyLink = $"http://localhost:5174/api/user/verifyotpbylink?email={toEmail}&emailOtp={otp}";
                string htmlBody = $@"
                <html>
         <body style='font-family:Arial, sans-serif; color:#333;'>
        <div style='max-width:600px; margin:auto; border:1px solid #ddd; padding:20px;'>
        <img src='cid:logoImage' alt='Epaint Logo' style='width:120px; margin-bottom:20px;' />
        <h2 style='color:#2c3e50;'>Welcome to Epaint 🎨</h2>
        <a href={verifyLink}>Verify</a>
        <p>Your one-time password (OTP) for completing registration is:</p>
        <h3 style='color:#e74c3c;'>{otp}</h3>
        <p>Epaint connects you with trusted contractors to make painting your home simple and hassle-free.</p>
        <p style='margin-top:20px;'>Use this code to verify your account and start exploring our network of professionals.</p>
        <hr />
        <p style='font-size:12px; color:#888;'>This is an automated message from Epaint. Please do not reply.</p>
       </div>
      </body>
        </html>";

                var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                // adding the epaint logo here
                var logo = new LinkedResource(
     @"C:\Users\2403951\OneDrive - Cognizant\Desktop\cognizant\projects\epaintGraphQl\assets\img6.jpg",
     MediaTypeNames.Image.Jpeg)
                {
                    ContentId = "logoImage"
                };
                htmlView.LinkedResources.Add(logo);


                mail.AlternateViews.Add(htmlView);
                client.Send(mail);
                Console.WriteLine("Email sents successfully");
                // end of the email sending in professional way 
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error sending mail {ex.Message}");
                throw;
            }
        }
    }
}