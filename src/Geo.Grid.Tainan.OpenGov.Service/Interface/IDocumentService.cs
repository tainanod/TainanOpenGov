using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IDocumentService
    {
        Result<Document> Upload(Guid id, UploadDoc uploadDoc);

        Result<Document> UploadForActivity(Guid id, UploadDoc uploadDoc);

        Result<Document> Remove(Guid id);

        
    }
}