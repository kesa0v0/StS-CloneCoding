using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerSO playerSO;

    void Start()
    {
        this.GetComponent<CharacterEntity>().Setup(playerSO.playerDatas[0]);
    }
}
