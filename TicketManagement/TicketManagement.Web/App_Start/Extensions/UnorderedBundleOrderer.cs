using System.Collections.Generic;
using System.Web.Optimization;

namespace TicketManagement.Web.Extensions
{
    public class UnorderedBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}