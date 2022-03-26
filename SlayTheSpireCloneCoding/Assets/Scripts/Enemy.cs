using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : CharacterEntity
{
    [SerializeField] TMP_Text IntendTMP;

    public void Setup(EnemyData enemyData)
    {
        Setup(new CharacterEntityData 
        { 
            name = enemyData.name, 
            sprite = enemyData.sprite,
            health = enemyData.health,
            isUseSanity = enemyData.isUseSanity,
            sanity = enemyData.sanity
        }); // override 왜 없지? 파이썬에선 있었는데 ㄹㅇㅋㅋ

        print("");
    }
}
