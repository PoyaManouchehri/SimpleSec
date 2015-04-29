using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SimpleSec.ClaimsTransformation.Transformations;

namespace SimpleSec.ClaimsTransformation
{
    public class ClaimsTransformer
    {
        protected IList<IClaimsTransformation> Transformations;

        public ClaimsTransformer()
        {
            Transformations = new List<IClaimsTransformation>();
        }

        public IEnumerable<Claim> Apply(IEnumerable<Claim> claims)
        {
            var claimList = claims.ToList();

            foreach (var trans in Transformations)
            {
                trans.Apply(claimList);
            }

            return claimList;
        }

        public void Copy(string sourceType, string targetType, bool overwrite = false)
        {
            Transform(new Copy(sourceType, targetType, overwrite));
        }

        public void SetDefaultValue(string type, string defaultValue)
        {
            Transform(new SetDefaultValue(type, defaultValue));
        }

        public void Map(string sourceType, string targetType, bool overwrite = false)
        {
            Transform(new Map(sourceType, targetType, overwrite));
        }

        public void Remove(string type)
        {
            Transform(new Remove(type));
        }

        public void Filter(params string[] claimTypesToKeep)
        {
            Transform(new Filter(claimTypesToKeep));
        }

        public void Transform(IClaimsTransformation transformation)
        {
            Transformations.Add(transformation);
        }
    }
}
