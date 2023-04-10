using System.Linq;
using UnityEngine;

namespace BoneAppetite;

public class Utilities
{
    internal static AssetBundle? LoadAssetBundle(string bundleName)
    {
        var resource = typeof(BoneAppetitMod).Assembly.GetManifestResourceNames().Single
            (s => s.EndsWith(bundleName));
        using var stream = typeof(BoneAppetitMod).Assembly.GetManifestResourceStream(resource);
        return AssetBundle.LoadFromStream(stream);
    }
}