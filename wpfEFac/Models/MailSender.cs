using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace wpfEFac.Models
{
    /// <summary>
    /// MailSender class is use to send e-mails and atach files to those e-mails.
    /// </summary>
    public class MailSender
    {
        /// <summary>
        /// This doesn´t do anything
        /// </summary>
        public MailSender()
        {

        }


        /// <summary>
        /// This Method is use to send a e-mail and from an Account to several revicers, also allows you to atach some files
        /// by giving the paths of those files.
        /// </summary>
        /// <param name="senderAccount">
        /// The e-mail Account use to send this e-mail message.
        /// </param>
        /// <param name="passwordOfSenderAccount">
        /// The Password of the senderAccount this is only to make the login.
        /// </param>
        /// <param name="recieversEmails">
        /// A set of e-mails that will recive the e-mail message.
        /// </param>
        /// <param name="subject">
        /// The subject of this e-mail message.
        /// </param>
        /// <param name="message">
        /// The message of this e-mail.
        /// </param>
        /// <param name="attachFiles">
        /// The path of the files to attach to this e-mail.
        /// </param>
        /// <param name="host">
        /// The host used by the SmtpClient
        /// </param>
        /// <param name="deliveryMethod">
        /// The SmtpDeliveryMethod enum that is use by the SmtpClient
        /// </param>
        /// <param name="enableSsl">
        /// Indacates if Ssl is enable or not
        /// </param>
        /// <param name="port">
        /// The number of port use in the SmtpClient
        /// </param>
        /// <returns>
        /// True if the e-mail is send.
        /// False if a Error happend.
        /// </returns>
        public static bool SendMail(string senderAccount, string passwordOfSenderAccount,
            List<string> recieversEmails, string subject, string message, List<string> attachFiles,
            string host, int port, bool ssl)
        {
            attachFiles.Remove(null);

            SmtpClient client = new SmtpClient
            {
                Host = host,
                Port = port,
                EnableSsl = ssl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderAccount, passwordOfSenderAccount)
                
            };  // This is our smtp client;

            MailMessage msg = new MailMessage(); // This represent the e-mail

            try
            { 

                msg.From = new MailAddress(senderAccount); // This add the sender email to the e-mail

                foreach (var emailAddress in recieversEmails) // Iterate through the list of revicers
                {
                    msg.To.Add(new MailAddress(emailAddress)); // Adds to the email
                }

                foreach (var item in attachFiles) // This attach the files to the email
                {
                    Attachment attachFile = new Attachment(item); // Creates Attchment
                    msg.Attachments.Add(attachFile); // Add to the email
                }

                msg.Subject = subject; //Adds the subject
                msg.Body = message; // Set the message
                msg.IsBodyHtml = true; // Say's that this is a body html

                client.Send(msg);

                return true;
            }
            catch (SmtpException ex)
            {
                SmtpStatusCode statusCode = ex.StatusCode;
                if (statusCode == SmtpStatusCode.MailboxBusy ||
                    statusCode == SmtpStatusCode.MailboxUnavailable ||
                    statusCode == SmtpStatusCode.TransactionFailed)
                {
                    // wait 5 seconds, try a second time
                    Thread.Sleep(5000);
                    client.Send(msg);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally 
            {
                msg.Dispose();
            }
        }

    }
}
