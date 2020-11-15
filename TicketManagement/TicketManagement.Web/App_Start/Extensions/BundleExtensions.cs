using System.Web.Optimization;

namespace TicketManagement.Web.Extensions
{
    internal static class BundleExtensions
    {
        public static Bundle UnorderedBundling(this Bundle bundle)
        {
            bundle.Orderer = new UnorderedBundleOrderer();
            return bundle;
        }
    }
}