using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("DistrictOffice", Schema = "dbo")]
    public class DistrictOffice
    {
        public DistrictOffice()
        {
            OfficeId = Guid.NewGuid();
        }

        ///<summary>
        /// 區公所編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"OfficeId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_DistrictOffice", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Office ID")]
        public System.Guid OfficeId { get; set; }

        ///<summary>
        /// 區公所名稱
        ///</summary>
        [Column(@"Name", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(500)]
        [StringLength(500)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        ///<summary>
        /// 經度
        ///</summary>
        [Column(@"Lng", Order = 3, TypeName = "float")]
        [Required]
        [Display(Name = "Lng")]
        public double Lng { get; set; }

        ///<summary>
        /// 緯度
        ///</summary>
        [Column(@"Lat", Order = 4, TypeName = "float")]
        [Required]
        [Display(Name = "Lat")]
        public double Lat { get; set; }

        
    }
}
