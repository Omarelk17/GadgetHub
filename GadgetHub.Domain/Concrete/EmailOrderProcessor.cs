using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;
using System.IO;

namespace GadgetHub.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        public class EmailSettings
        {
            public static string MailToAddress = "orders@omar.com";
            public static string MailFromAddress = "admin@GadgetHub.com";
            public bool UseSsl = true;
            public string Username = "MySmtpUsername";
            public string Password = "MySmtpPassword";
            public string ServerName = "smtp.omar.com";
            public int ServerPort = 587;
            public bool WriteAsFile = false;
            public string FileLocation = @"c:\gadgethub_emails";
        }

        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;

                    if (!Directory.Exists(emailSettings.FileLocation))
                    {
                        Directory.CreateDirectory(emailSettings.FileLocation);
                    }
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("==========")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c})", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("-----------")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("Gift wrap: " + (shippingInfo.GiftWrap ? "Yes" : "No"));

                MailMessage mailMessage = new MailMessage(
                    EmailSettings.MailFromAddress,
                    EmailSettings.MailToAddress,
                    "New order submitted!",
                    body.ToString());

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
