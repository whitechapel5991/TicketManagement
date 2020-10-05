// ****************************************************************************
// <copyright file="GmailHelper.cs" company="EPAM Systems">
// Copyright (c) EPAM Systems. All rights reserved.
// Author Dzianis Shcharbakou.
// </copyright>
// ****************************************************************************

using System;
using System.Net;
using System.Net.Mail;
using TicketManagement.BLL.Infrastructure.Helpers.Interfaces;

namespace TicketManagement.BLL.Infrastructure.Helpers
{
    public class GmailHelper : IEmailHelper
    {
        private const string Hostname = "smtp.gmail.com";
        private const int Port = 587;
        private const bool UseSsl = true;
        private readonly string email;
        private readonly string emailPassword;

        public GmailHelper(string email, string password)
        {
            this.email = email;
            this.emailPassword = password;
        }

        public void SendEmail(string recipient, string subject, string htmlMessage)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(this.email);
                    mailMessage.To.Add(recipient);
                    mailMessage.Subject = subject;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = htmlMessage;
                    using (SmtpClient smtp = new SmtpClient(Hostname, Port))
                    {
                        smtp.EnableSsl = UseSsl;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(this.email, this.emailPassword);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 20000;
                        smtp.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
