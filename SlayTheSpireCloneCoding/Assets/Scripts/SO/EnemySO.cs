using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyData : CharacterEntityData
{
    public EnemyPattern[] pattern; // TODO: 일단 임시로 넣어둔 패턴. 이거 이렇게 해도 될려나? 아님 나중에 따로 적들 스크립트 만들어야 할지도
}

[CreateAssetMenu(fileName ="EnemySO", menuName = "Scriptable Object/EnemySO")]
public class EnemySO : ScriptableObject
{
    public EnemyData[] enemyDatas;
}
