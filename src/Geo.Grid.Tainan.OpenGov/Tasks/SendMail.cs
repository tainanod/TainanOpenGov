using System;
using Quartz;
using Geo.Grid.Tainan.OpenGov.Service;

namespace Geo.Grid.Tainan.OpenGov.Tasks
{
    /// <summary>
    /// 排程-mail發送
    /// </summary>
    [DisallowConcurrentExecution]
    public class SendMail : IJob
    {
        /// <summary>
        /// 執行發送郵件工作
        /// </summary>
        /// <param name="context">context</param>
        public void Execute(IJobExecutionContext context)
        {
            try
            {                
                var _service = new WaitingMailService();
                var data = _service.GetSendMail();
            }
            catch (Exception e)
            {
                var xx = e.Message;
            }
        }
    }
}