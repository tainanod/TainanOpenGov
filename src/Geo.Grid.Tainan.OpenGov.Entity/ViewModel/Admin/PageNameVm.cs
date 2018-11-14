using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// string & name
    /// </summary>
    public class PageNameVm
    {
        /// <summary>
        /// id
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PageId { get; set; } = string.Empty;

        /// <summary>
        /// name
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PageName { get; set; } = string.Empty;

        /// <summary>
        /// url
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PageUrl { get; set; } = string.Empty;
    }
}
