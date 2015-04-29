using System.Collections.Generic;
using System.Security.Claims;
using SimpleSec.ClaimsTransformation.Extensions;

namespace SimpleSec.ClaimsTransformation.Transformations
{
    internal class Copy : IClaimsTransformation
    {
        private readonly string _sourceType;
        private readonly string _targetType;
        private readonly bool _overwrite;

        public Copy(string sourceType, string targetType, bool overwrite)
        {
            _sourceType = sourceType;
            _targetType = targetType;
            _overwrite = overwrite;
        }

        public virtual void Apply(IList<Claim> claims)
        {
            var sourceClaim = claims.Find(_sourceType);
            if (sourceClaim == null)
                return;

            var existingTargetClaim = claims.Find(_targetType);
            if (existingTargetClaim != null)
            {
                if (!_overwrite)
                    return;

                claims.Remove(existingTargetClaim);
            }

            claims.Add(new Claim(_targetType, sourceClaim.Value));
        }
    }
}
