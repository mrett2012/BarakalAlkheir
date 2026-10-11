using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace BarakalAlkheir.Model
{
    internal sealed class BarakeitAlkheirDbConfiguration : DbConfiguration
    {
        public BarakeitAlkheirDbConfiguration()
        {
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
        }
    }
}
