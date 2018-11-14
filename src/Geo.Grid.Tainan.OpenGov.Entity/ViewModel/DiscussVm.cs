using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class DiscussVm 
    {
        public DiscussVm()
        {
            TagIds = new List<Guid>();
            Tags = new List<TagVm>();
            PushUserIds = new List<string>();
            AspNetUser = new AspNetUserVm();
        }

        public Guid DiscussId { get; set; }

        [Required]
        public Guid ForumId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public Guid? UpperId { get; set; }

        public int ReplyAmount { get; set; }

        [Required]
        [StringLength(4000)]
        public string Message { get; set; }

        /// <summary>
        /// 置頂
        /// </summary>
        public bool IsTop { get; set; } = false;

        public List<Guid> TagIds { get; set; }

        public IEnumerable<TagVm> Tags { get; set; }

        public IEnumerable<string> PushUserIds { get; set; }

        public DateTime CreatedDate { get; set; }

        public AspNetUserVm AspNetUser { get; set; }
    }
}