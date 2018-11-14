using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class ActivityVm
    {
        public Guid ActivityId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string HoldDate { get; set; }

        public string Location { get; set; }

        public ActivityType ActivityType { get; set; }

        public List<IdName> Attachments { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}