using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mdm.Core.Extensions
{
    internal static class CollectionExtensions
    {
        public static async Task<IList<T>> ToListAsync<T>(this IEnumerable<T> source)
        {
            return await Task.Factory.StartNew(() => source.ToList());
        }
    }
}
