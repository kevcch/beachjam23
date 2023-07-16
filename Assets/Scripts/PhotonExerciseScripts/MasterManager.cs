using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    //[SerializeField]
    //private GameSettings gamesettings;
    //public static GameSettings GameSettings
    //{
    //    get { return Instance.gamesettings; }
    //}

    //[serializefield]
    //private list<networkprefab> networkprefabs = new list<networkprefab>();

    //public static gameobject networkinstantiate(gameobject obj, vector3 position, quaternion rotation)
    //{
    //    foreach (networkprefab networkprefab in instance.networkprefabs)
    //    {
    //        if (networkprefab.prefab == obj)
    //        {
    //            if (networkprefab.path == string.empty)
    //            {
    //                debug.logerror("pathname is empty, cannot instantiate object!");
    //                return null;
    //            }

    //            gameobject result = photonnetwork.instantiate(networkprefab.path, position, rotation);
    //            return result;
    //        }
    //    }
    //    return null;
    //}

//    // note: at least run once in unity editor if update the resource folder
//    [runtimeinitializeonloadmethod(runtimeinitializeloadtype.beforesceneload)]
//    public static void populatenetworkprefabs()
//    {
//#if unity_editor

//            instance.networkprefabs.clear();

//            gameobject[] results = resources.loadall<gameobject>("");

//            for(int i = 0; i < results.length; i++)
//            {
//                if (results[i].getcomponent<photonview>() != null)
//                {
//                    string path = assetdatabase.getassetpath(results[i]);
//                    instance.networkprefabs.add(new networkprefab(results[i], path));
//                }
//            }
//#endif
//    }
}
