using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace Domain.Concrete.OrderProcessors
{

    public class EmailSettings
    {
        public string MailToAddress = "My.Lord.Land@gmail.com";
        public string MailFromAddress = "fil_dv1@bigmir.net";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"d:\ilandLord_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessorOrder(Cart cart, ShippingDetails shippingDetails)
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
                }

                StringBuilder body = new StringBuilder()
                .AppendLine("Получена новая заявка!")
                .AppendLine("-------------------------------")
                .AppendLine("Интересующие объекты:")
                .AppendLine();

                foreach (var line in cart.Lines)
                {
                    //var subtotal = line.Book.Price * line.Quantity;
                    body.AppendFormat("ID объекта: {0} , Адрес объекта: \"{1}\"",
                                    line.Area.AreaID, line.Area.LegalAddressRegion + line.Area.RentAreaAddressCity + line.Area.RentAreaAddressStreet)
                    .AppendLine()
                    .AppendLine();
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.CalculateTotalSum())
                    .AppendLine()
                    .AppendLine("-------------------------------")
                    .AppendLine("Имя клиента:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine("Контактный телефон:")
                    .AppendLine(shippingDetails.Phone);
                

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Получена новая заявка!",
                    body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }        
    }
}
