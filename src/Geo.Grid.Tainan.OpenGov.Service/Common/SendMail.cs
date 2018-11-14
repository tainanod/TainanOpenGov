using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using Geo.Grid.Tainan.OpenGov.Entity.Common;

namespace Geo.Grid.Tainan.OpenGov.Service.Common
{
    /// <summary>
    /// 信件管理
    /// </summary>
    public class SendMail
    {
        /// <summary>
        /// getMail
        /// </summary>
        /// <param name="sendTo">收件者</param>
        /// <param name="subJect">主旨</param>
        /// <param name="mailContent">內容</param>
        /// <param name="sendBcc">cc</param>
        /// <returns></returns>
        public static Result<string> GetSendMail(string sendTo, string subJect, string mailContent, string sendBcc)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                string mailService = "smtp.tainan.gov.tw";
                string mailAccount = "opengov@mail.tainan.gov.tw";
                string mailPassword = "opengov2016";
                string mailUserName = "merace1984@mail.tainan.gov.tw";
                int mailPort = 25;

#if DEBUG
                subJect = "【工程師測試中】" + subJect;
                sendTo = "joehsu@geo.com.tw";
                sendBcc = "joe24267@gmail.com,nella@geo.com.tw,mike@geo.com.tw";
#endif

                SmtpClient sc = new SmtpClient(mailService);
                sc.Port = mailPort;
                sc.Credentials = new NetworkCredential(mailAccount, mailPassword);
                sc.EnableSsl = false;

                MailMessage mail = new MailMessage();
                //寄件者
                mail.From = new MailAddress("臺南開放政府<" + mailUserName + ">");

                //收件者
                mail.To.Add(sendTo);
                if (!string.IsNullOrEmpty(sendBcc))
                {
                    mail.Bcc.Add(sendBcc);
                }
                mail.Subject = subJect;
                mail.Body = mailContent;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = UTF8Encoding.UTF8;
                mail.BodyEncoding = UTF8Encoding.UTF8;

                sc.Send(mail);
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
    }
}
