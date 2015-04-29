using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SimpleSec.ClaimsTransformation.Extensions
{
    internal static class ClaimsExtensions
    {
        public static Claim Find(this IEnumerable<Claim> claims, string type)
        {
            return claims == null
                ? null
                : claims.FirstOrDefault(c => c.Type.Equals(type, StringComparison.CurrentCultureIgnoreCase));
        }

        public static bool Contains(this IEnumerable<Claim> claims, string type)
        {
            return claims.Find(type) != null;
        }
    }
}
