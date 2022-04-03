using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DealDamage : Effect
{
    [SerializeField] int[] _DamagePerLevel = new int[] { 5, 8 };

    public override string CardDescription()
    {
        return $"대상에게 { this._DamagePerLevel[this.ReinforcedLevel] } 데미지를 가합니다"; //string.format
    }
    public override void ApplyEffect()
    {
        EntityManager.Inst.getDamage(target, this._DamagePerLevel[this.ReinforcedLevel]);
    }
}