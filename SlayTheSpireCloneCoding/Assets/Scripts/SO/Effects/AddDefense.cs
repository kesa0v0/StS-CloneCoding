using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName ="AddDefense", menuName = "Scriptable Object/Effect/AddDefense")]
public class AddDefense : Effect
{
    public override string CardDescription()
    {
        return "대상에게 추가 체력을 제공합니다"; //string.format
    }
    public override void ApplyEffect()
    {
        target.health += amount;
    }
}