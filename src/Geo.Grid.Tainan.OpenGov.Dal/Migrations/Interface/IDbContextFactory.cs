namespace Geo.Grid.Tainan.OpenGov.Dal.Interface
{
    public interface IDbContextFactory
    {
        OpenGovContext GetDbContext();
    }
}