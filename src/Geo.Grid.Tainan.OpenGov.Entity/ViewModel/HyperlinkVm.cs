using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    public class HyperlinkVm
    {
        public Guid HyperlinkId { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public bool IsClick { get; set; } = false;
    }
}