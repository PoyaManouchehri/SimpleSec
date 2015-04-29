using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SimpleSec.ClaimsTransformation.Transformations
{
    internal class Filter : IClaimsTransformation
    {
        private readonly string[] _claimsTypesToKeep;

        public Filter(params string[] claimsTypesToKeep)
        {
            _claimsTypesToKeep = claimsTypesToKeep;
        }

        public void Apply(IList<Claim> claims)
        {
            for (var i = claims.Count - 1; i >= 0; i--)
            {
                if (!_claimsTypesToKeep.Any(ct => claims[i].Type.Equals(ct, StringComparison.CurrentCultureIgnoreCase)))
                {
                    claims.RemoveAt(i);
                }
            }
        }
    }
}
