using System.Collections.Generic;
using System.Security.Claims;

namespace SimpleSec.ClaimsTransformation
{
    public interface IClaimsTransformation
    {
        void Apply(IList<Claim> claims);
    }
}
