using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; }}

    [SerializeField]
    private string _nickName = "Player";
    public string NickName { 
        get {
            string id_str = "";
            for(int i = 0; i < 6; i++)
            {
                id_str += Random.Range(0, 9).ToString();
            }
            
            return _nickName + "_" + id_str; 
        }
    }
}
