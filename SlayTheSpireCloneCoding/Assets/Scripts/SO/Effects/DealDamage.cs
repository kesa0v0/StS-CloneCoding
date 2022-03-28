using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName ="DealDamage", menuName = "Scriptable Object/Effect/DealDamage")]
public class DealDamage : Effect
{
    public override string CardDescription()
    {
        return "대상에게 데미지를 가합니다"; //string.format
    }
    public override void ApplyEffect()
    {
        target.health -= amount;
    }
}