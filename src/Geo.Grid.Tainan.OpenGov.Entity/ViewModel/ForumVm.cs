using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class ForumVm
    {
        public Guid ForumId { get; set; }

        public string Category { get; set; }

        public string Subject { get; set; }

        public string Holder { get; set; }
        //public Guid Holder { get; set; }

        public string Description { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime CloseDate { get; set; }

        public Guid? UpperId { get; set; }

        public string Announcement { get; set; }


        public IEnumerable<ActivityVm> ForumActivity { get; set; }

        public IEnumerable<ActivityVm> WorkActivity { get; set; }

        public IEnumerable<DocumentVm> NormalDocuments { get; set; }

        public IEnumerable<DocumentVm> GovReplyDocuments { get; set; }

        public IEnumerable<HyperlinkVm> HyperLinks { get; set; }

        public IEnumerable<PhotoVm> Photos { get; set; }
        public int DiscussCount { get; set; }
        public int DiscussAmount { get; set; }
    }
}
