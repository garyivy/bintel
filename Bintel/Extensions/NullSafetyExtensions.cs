using System;
using System.Collections.Generic;
using System.Linq;

namespace Bintel.Extensions
{
    public static class NullSafetyExtensions
    {
        public static IEnumerable<T> NullSafe<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }
    }
}
