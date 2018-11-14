using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// guid & Name
    /// </summary>
    public class GuidNameVm
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid PageGuid { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string PageName { get; set; } = string.Empty;
    }
}
