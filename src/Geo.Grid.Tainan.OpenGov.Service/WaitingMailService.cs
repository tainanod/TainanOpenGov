using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Entity;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 寄信管理-排程用
    /// </summary>
    public class WaitingMailService
    {
        private OpenGovContext _db = new OpenGovContext();

        /// <summary>
        /// 寄信
        /// </summary>
        /// <returns></returns>
        public Result<string> GetSendMail()
        {
            Result<string> result = new Result<string>(false);
            try
            {
                bool bt = false;
                var data = _db.WaitingMail.Where(x => !x.IsSend).ToList();
                foreach (var item in data)
                {
                    bt = SendMail.GetSendMail(item.ToEmail, item.Subject, item.MailContent, item.BccEmail).Success;
                    if (bt)
                    {
                        item.IsSend = true;
                        item.SendTime = DateTime.Now;
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #region 公民論壇-回覆留言-管理者用

        /// <summary>
        /// 信件管理-每日16:30寄信給管理者
        /// 回覆的
        /// </summary>
        /// <returns></returns>
        public Result<string> GetDiscussReplyAdminEmail()
        {
            Result<string> result = new Result<string>(false);
            try
            {
                DateTime startDate = Convert.ToDateTime(DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd 16:30"));
                DateTime endDate = Convert.ToDateTime(DateTime.Today.ToString("yyyy/MM/dd 16:30"));

                var data = _db.Discuss.OrderBy(x => x.CreatedDate).Where(x => x.IsEnabled && x.CreatedDate >= startDate && x.CreatedDate <= endDate);
                using (FileStream fs = File.Open(Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "App_Data/ForumReplyAdmin.html"), FileMode.Open))
                {
                    string mailBody = string.Empty;
                    StreamReader sr = new StreamReader(fs);
                    mailBody = sr.ReadToEnd();

                    string htmlArray = string.Empty;
                    foreach (var item in data)
                    {
                        htmlArray += GetMailAdminHtml(item.Forum.Subject, item.AspNetUsers.NickName, item.Message, item.ForumId, (item.UpperId.HasValue ? item.UpperId.Value : item.DiscussId));
                    }
                    mailBody = mailBody.Replace(@"{{DateTime}}", DateTime.Today.ToString("yyyy/MM/dd"));
                    mailBody = mailBody.Replace(@"{{trList}}", htmlArray);

                    var mailData = new WaitingMail();
                    mailData.TypeName = "公民論壇-討論區回覆";
                    mailData.ToName = "merace1984";
                    mailData.ToEmail = "merace1984@mail.tainan.gov.tw";
                    mailData.BccEmail = "";
                    mailData.Subject = "臺南開放政府-您有一封回覆留言" + DateTime.Today.ToString("(yyyy/MM/dd)");
                    mailData.MailContent = mailBody;
                    if (!string.IsNullOrEmpty(htmlArray))
                    {
                        _db.WaitingMail.Add(mailData);
                        _db.SaveChanges();
                    }

                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 信件管理-每日16:30寄信給管理者
        /// 回覆的
        /// </summary>
        /// <returns></returns>
        public Result<string> GetParticipationDiscussReplyAdminEmail()
        {
            Result<string> result = new Result<string>(false);
            try
            {
                DateTime startDate = Convert.ToDateTime(DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd 16:30"));
                DateTime endDate = Convert.ToDateTime(DateTime.Today.ToString("yyyy/MM/dd 16:30"));

                var data = _db.ParticipationDiscusses.OrderBy(x => x.CreatedDate).Where(x => x.IsEnabled && x.CreatedDate >= startDate && x.CreatedDate <= endDate).ToList();
               
                using (FileStream fs = File.Open(Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "App_Data/ForumReplyAdmin.html"), FileMode.Open))
                {
                    string mailBody = string.Empty;
                    StreamReader sr = new StreamReader(fs);
                    mailBody = sr.ReadToEnd();

                    string htmlArray = string.Empty;
                    
                    foreach (var item in data)
                    {
                        var subject = _db.Participations.Where(x => x.ParticipationId == item.ParticipationId).FirstOrDefault()?.Subject;
                        htmlArray += GetMailAdminHtml(subject, item.AspNetUser.NickName, item.Message, item.ParticipationId, (item.UpperId.HasValue ? item.UpperId.Value : item.DiscussId));
                    }
                    mailBody = mailBody.Replace(@"{{DateTime}}", DateTime.Today.ToString("yyyy/MM/dd"));
                    mailBody = mailBody.Replace(@"{{trList}}", htmlArray);

                    var mailData = new WaitingMail();
                    mailData.TypeName = "市政參與-討論區回覆";
                    mailData.ToName = "merace1984";
                    mailData.ToEmail = "merace1984@mail.tainan.gov.tw";
                    mailData.BccEmail = "";
                    mailData.Subject = "臺南開放政府-您有一封回覆留言" + DateTime.Today.ToString("(yyyy/MM/dd)");
                    mailData.MailContent = mailBody;
                    if (!string.IsNullOrEmpty(htmlArray))
                    {
                        _db.WaitingMail.Add(mailData);
                        _db.SaveChanges();
                    }

                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 信件管理-每日16:30寄信給管理者
        /// html
        /// </summary>
        /// <param name="title">議題</param>
        /// <param name="userName">nickName</param>
        /// <param name="userMessage">message</param>
        /// <param name="forumId">forumId</param>
        /// <param name="upperId">upperId | DiscussId</param>
        /// <returns></returns>
        private string GetMailAdminHtml(string title, string userName, string userMessage, Guid forumId, Guid upperId)
        {
            string html = string.Empty;
            string urlName = "http://" + System.Web.HttpContext.Current.Request.Url.Authority;
            if (urlName.Contains("demo2"))
            {
                urlName += "/OpenGov";
            }

            html += "<tr>";
            html += "   <td>" + title + "</td>";
            html += "   <td>" + userName + "</td>";
            html += "   <td>" + HtmlSplit.SplitWord(userMessage, 50, "..") + "</td>";
            html += "   <td><a href='" + urlName + "/Forum/Detail/" + forumId + "?did=" + upperId + "' target='_blank'>連結</a></td>";
            html += "</tr>";

            return html;
        }

        #endregion
    }
}
