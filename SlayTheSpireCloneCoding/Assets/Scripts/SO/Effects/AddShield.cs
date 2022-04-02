using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AddShield : Effect
{
    int[] _shieldPerLevel = new int[] { 5, 8 };

    public override string CardDescription()
    {
        return $"대상에게 방어력 { this._shieldPerLevel[this.ReinforcedLevel] } 을 제공합니다"; //string.format
    }
    public override void ApplyEffect()
    {
        target.shield += _shieldPerLevel[ReinforcedLevel];
    }
}