using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfEFac.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;
    using System.Net.Mail;
    using System.Threading;


        /// <summary>
        /// WPF Email Class
        /// </summary>
        public class WPFEmailer
        {
            public string From = string.Empty;
            public string To = string.Empty;
            public string User = string.Empty;
            public string Password = string.Empty;
            public string Subject = string.Empty;
            public string Body = string.Empty;
            public string AttachmentPath1 = string.Empty;
            public string AttachmentPath2 = string.Empty;
            public string Host = "127.0.0.1";
            public int Port = 25;
            
            public string ccRespaldo = string.Empty;
            public string ccContador = string.Empty;
            public string ccAdesoft = string.Empty;

            public bool IsHtml = false;
            public int SendUsing = 0;//0 = Network, 1 = PickupDirectory, 2 = SpecifiedPickupDirectory
            public bool UseSSL = true;
            public int AuthenticationMode = 1;//0 = No authentication, 1 = Plain Text, 2 = NTLM authentication

            public WPFEmailer()
            {

            }

            public void SendEmail()
            {
                new Thread(new ThreadStart(SendMessage)).Start();
            }
            /// <summary>
            /// Send Email Message method.
            /// </summary>
            private void SendMessage()
            {
                try
                {
                    MailMessage oMessage = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient(Host);

                    oMessage.From = new MailAddress(From);
                    oMessage.To.Add(To);
                    oMessage.Subject = Subject;
                    oMessage.IsBodyHtml = IsHtml;
                    oMessage.Body = Body;

                    //if (ccC != string.Empty)
                    //{
                        
                        oMessage.CC.Add(ccContador);
                        oMessage.CC.Add(ccRespaldo);
                        oMessage.CC.Add(ccAdesoft);
                    //}

                    switch (SendUsing)
                    {
                        case 0:
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            break;
                        case 1:
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                            break;
                        case 2:
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                            break;
                        default:
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            break;

                    }
                    if (AuthenticationMode > 0)
                    {
                        smtpClient.Credentials = new NetworkCredential(User, Password);
                    }

                    smtpClient.Port = Port;
                    smtpClient.EnableSsl = UseSSL;

                    // Create and add the attachment
                    if (AttachmentPath1 != string.Empty)
                    {
                        oMessage.Attachments.Add(new Attachment(AttachmentPath1));
                    }

                    // Create and add the attachment
                    if (AttachmentPath2 != string.Empty)
                    {
                        oMessage.Attachments.Add(new Attachment(AttachmentPath2));
                    }
                    try
                    {
                        // Deliver the message    
                        smtpClient.Send(oMessage);

                    }

                    catch (Exception ex)
                    {
                        ex.ToString();

                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

        }
    
}
