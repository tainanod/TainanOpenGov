using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class DocumentVm
    {
        public Guid DocumentId { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 是否已點擊
        /// </summary>
        public bool IsClick { get; set; } = false;
    }
}