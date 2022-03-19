using Employee.DBCore.Context.EFContext;

namespace Employee.DBCore.Factory
{
    public interface IContextFactory
    {
        IDatabaseContext DbContext { get; }
    }
}
