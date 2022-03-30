using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AddDefense : Effect
{
    int[] _damagePerLevel = new int[] { 5, 8 };

    public override string CardDescription()
    {
        return $"대상에게 추가 체력 { this._damagePerLevel[this.ReinforcedLevel] } 을 제공합니다"; //string.format
    }
    public override void ApplyEffect()
    {
        target.health += _damagePerLevel[ReinforcedLevel];
    }
}