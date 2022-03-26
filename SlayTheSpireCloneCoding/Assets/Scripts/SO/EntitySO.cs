using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterEntityData
{
    public string name;
    public Sprite sprite;
    public int health;
    public bool isUseSanity;
    public int sanity;
}

[System.Serializable]
public class EnemyData : CharacterEntityData
{
    public int[] pattern; // TODO: 일단 임시로 넣어둔 패턴. 이거 이렇게 해도 될려나? 아님 나중에 따로 적들 스크립트 만들어야 할지도
}


[CreateAssetMenu(fileName ="EnemySO", menuName = "Scriptable Object/EnemySO")]
public class EntitySO : ScriptableObject
{
    public EnemyData[] enemyDatas;
}
