using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Controllers
{
    public class ResourceController : Controller
    {
        private IResourceService service;

        public ResourceController(IResourceService service)
        {
            this.service = service;
        }

        public FileContentResult Photo(Guid id, PhotoSize size = PhotoSize.Width120)
        {
            byte[] jpg = service.GetPhotoWithSize(id, size);
            return new FileContentResult(jpg, System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }

        public FileContentResult Document(Guid id)
        {
            Document doc = service.GetDocument(id);
            if (doc != null)
            {
                var click = service.GetDocumentClickSave(id);
                return File(doc.DocumentExt.Original, doc.FileType);
            }
            return null;
        }

        public FileContentResult ParticipationDocument(Guid id)
        {
            ParticipationDocument doc = service.GetParticipationDocument(id);
            if (doc != null)
            {
                var click = service.GetParticipationDocumentClickSave(id);
                return File(doc.ParticipationDocumentExt.Original, doc.FileType);
            }
            return null;
        }
    }
}