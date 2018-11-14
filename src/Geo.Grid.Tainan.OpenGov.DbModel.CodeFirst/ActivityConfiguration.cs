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
    public class ActivityConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Activity>
    {
        public ActivityConfiguration()
            : this("dbo")
        {
        }

        public ActivityConfiguration(string schema)
        {
            HasMany(t => t.Documents).WithMany(t => t.Activities).Map(m =>
            {
                m.ToTable("REL_ACTIVITY_DOCUMENT", "dbo");
                m.MapLeftKey("ACTIVITY_ID");
                m.MapRightKey("DOCUMENT_ID");
            });
        }
    }

}
// </auto-generated>
