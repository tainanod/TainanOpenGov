using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class UploadDoc
    {
        public HttpPostedFileBase PostedFile { get; set; }
        public string Name { get; set; }
        public string Alt { get; set; }
        public DocType DocType { get; set; }
    }
}
