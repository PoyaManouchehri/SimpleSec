using System.Collections.Generic;
using System.Security.Claims;
using SimpleSec.ClaimsTransformation.Extensions;

namespace SimpleSec.ClaimsTransformation.Transformations
{
    internal class SetDefaultValue : IClaimsTransformation
    {
        private readonly string _type;
        private readonly string _defaultValue;

        public SetDefaultValue(string type, string defaultValue)
        {
            _type = type;
            _defaultValue = defaultValue;
        }

        public void Apply(IList<Claim> claims)
        {
            if (claims.Contains(_type))
                return;

            claims.Add(new Claim(_type, _defaultValue));
        }
    }
}
