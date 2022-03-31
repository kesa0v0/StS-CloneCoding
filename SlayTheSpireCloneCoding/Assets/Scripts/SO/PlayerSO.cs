using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData : CharacterEntityData
{

}

[CreateAssetMenu(fileName ="PlayerSO", menuName = "Scriptable Object/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public PlayerData[] playerDatas;
}
