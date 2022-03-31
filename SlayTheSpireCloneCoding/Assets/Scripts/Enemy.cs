using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy:MonoBehaviour
{
    public GameObject Base;
    [SerializeField] TMP_Text IntendTMP;

    public void Setup(EnemyData enemyData)
    {
        CharacterEntity cEntity = Base.GetComponent<CharacterEntity>();

        cEntity.Setup(enemyData);

        print("asdf");
    }
}
