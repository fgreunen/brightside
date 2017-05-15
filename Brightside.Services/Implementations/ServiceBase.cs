using Brightside.Common;
using Brightside.Data;

namespace Brightside.Services.Implementations
{
    public abstract class ServiceBase
        : DisposableBase
    {
        protected BrightsideEntities Entities
        {
            get
            {
                return new BrightsideEntities();
            }
        }

        protected IConfiguration Configuration
        {
            get
            {
                return Kernel.Get<IConfiguration>();
            }
        }
    }
}