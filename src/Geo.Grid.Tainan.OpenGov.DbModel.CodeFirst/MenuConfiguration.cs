// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.DbModel.CodeFirst
{

    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class MenuConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
            : this("dbo")
        {
        }

        public MenuConfiguration(string schema)
        {
            Property(x => x.Area).IsOptional();
            Property(x => x.CreatedBy).IsOptional();
            Property(x => x.UpdatedBy).IsOptional();
            HasMany(t => t.AspNetRoles).WithMany(t => t.Menus).Map(m =>
            {
                m.ToTable("REL_AspNetRoles_Menu", "dbo");
                m.MapLeftKey("MenuId");
                m.MapRightKey("RoleId");
            });
        }
    }

}
// </auto-generated>
