using System;
using Quartz;
using Geo.Grid.Tainan.OpenGov.Service;

namespace Geo.Grid.Tainan.OpenGov.Tasks
{
    /// <summary>
    /// 排程-記錄公民論壇回覆給管理員
    /// </summary>
    [DisallowConcurrentExecution]
    public class SaveDiscussMailAdmin : IJob
    {
        /// <summary>
        /// 執行儲存工作
        /// </summary>
        /// <param name="context">context</param>
        public void Execute(IJobExecutionContext context)
        {
            try
            {                
                var _service = new WaitingMailService();               
                var data = _service.GetDiscussReplyAdminEmail();
            }
            catch (Exception e)
            {
                var xx = e.Message;
            }
        }
    }
}