using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DealDamage : Effect
{
    int[] _ShieldPerLevel = new int[] { 5, 8 };

    public override string CardDescription()
    {
        return $"대상에게 { this._ShieldPerLevel[this.ReinforcedLevel] } 데미지를 가합니다"; //string.format
    }
    public override void ApplyEffect()
    {
        target.health -= _ShieldPerLevel[ReinforcedLevel];
    }
}