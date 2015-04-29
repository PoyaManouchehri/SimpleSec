#SimpleSec.ClaimsTransformation

This is a simple helper for transforming a list of claims. Here is a sample of how you can use it:

```C#
var transformer = new ClaimsTransformer();

// copy from source to target
transformer.Copy("SourceClaim", "TargetClaim", overwrite: true);

// map from source to target (removes source)
transformer.Map("SourceClaim", "TargetClaim", overwrite: true);

// remove all claims except these 3
transformer.Filter("ClaimA", "ClaimB", "ClaimC");

// remove ClaimA
transformer.Remove("ClaimA");

// set a default value for ClaimB
transformer.SetDefaultValue("ClaimB", "DefaultValue");

var transformedClaims = transformer.Apply(_claims);
```

You can also do custom transformations by implementing the `IClaimsTransformation` interface, and then adding it in a similar way:

```C#
class MyTransformation : IClaimsTransformation
{
    public void Apply (IList<Claim> claims)
    {
        // modify the list here;
    }
}

transformer.Transform(new MyTransformation());
```
