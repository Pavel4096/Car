using System.IO;
using UnityEditor;

public class BuildAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    private static void StartAssetBundlesBuild()
    {
        const string assetbundlesfolder = "Assets/AssetBundles";

        if(!Directory.Exists(assetbundlesfolder))
            Directory.CreateDirectory(assetbundlesfolder);

        BuildPipeline.BuildAssetBundles(assetbundlesfolder, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
    }
}
