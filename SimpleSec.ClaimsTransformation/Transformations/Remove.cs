using System.Collections.Generic;
using System.Security.Claims;
using SimpleSec.ClaimsTransformation.Extensions;

namespace SimpleSec.ClaimsTransformation.Transformations
{
    internal class Remove : IClaimsTransformation
    {
        private readonly string _type;

        public Remove(string type)
        {
            _type = type;
        }

        public void Apply(IList<Claim> claims)
        {
            var claimToRemove = claims.Find(_type);
            if (claimToRemove != null)
                claims.Remove(claimToRemove);
        }
    }
}
