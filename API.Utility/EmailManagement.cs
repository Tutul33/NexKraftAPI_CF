using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Settings;

namespace API.Utility
{
    public class EmailManagement
    {
        public async Task<bool> Sendemail(string emailto, string title, string subject, string emailBody)
        {
            bool result = false; string ccMail = string.Empty;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(new MailAddress(emailto));
                    //if (CC=true)
                    //{
                    //    ccMail ="demo@gmail.com";
                    //    mail.CC.Add(new MailAddress(ccMail));
                    //}
                    mail.Subject = subject;
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;
                    MailAddress mailaddress = new MailAddress(StaticInfos.SenderId, title);
                    mail.From = mailaddress;



                    using (SmtpClient smtp = new SmtpClient("smtp.titan.email"))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(StaticInfos.SenderId, StaticInfos.SenderPassword);

                        smtp.EnableSsl = Convert.ToBoolean(StaticInfos.EnableSSL);
                        smtp.Port = Convert.ToInt32(StaticInfos.SenderPort);
                        await smtp.SendMailAsync(mail);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
