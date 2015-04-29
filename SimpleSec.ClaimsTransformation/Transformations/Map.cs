using System.Collections.Generic;
using System.Security.Claims;
using SimpleSec.ClaimsTransformation.Extensions;

namespace SimpleSec.ClaimsTransformation.Transformations
{
    internal class Map : Copy
    {
        private readonly string _sourceType;

        public Map(string sourceType, string targetType, bool overwrite)
            : base(sourceType, targetType, overwrite)
        {
            _sourceType = sourceType;
        }

        public override void Apply(IList<Claim> claims)
        {
            base.Apply(claims);

            var sourceClaim = claims.Find(_sourceType);
            if (sourceClaim != null)
                claims.Remove(sourceClaim);
        }
    }
}
