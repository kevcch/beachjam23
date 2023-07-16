using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkPrefab
{
    public GameObject Prefab;
    public string Path;

    public NetworkPrefab(GameObject prefab, string path)
    {
        Prefab = prefab;
        Path = ModifyPrefabPathname(path);
    }

    private string ModifyPrefabPathname(string path)
    {
        int extensionLength = System.IO.Path.GetExtension(path).Length;
        int startIndex = path.ToLower().IndexOf("resources") + 10;

        if (startIndex < 0)
        {
            return string.Empty;
        }
        
        return path.Substring(startIndex, path.Length - (startIndex + extensionLength));


    }
}
