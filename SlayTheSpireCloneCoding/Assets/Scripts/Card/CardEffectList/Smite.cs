using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : CardData
{
    public Smite() {
        cardName = "강타";
        energy = 2;
        reinforcedLevel = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_5");
    }

    int[] _damagePerReinforce = new int[] { 8, 12 };

    public override string CardDescription()
    {
        return $"대상에게 { _damagePerReinforce[reinforcedLevel] } 데미지를 가하고 { "취약" }을 { 2 } 부여합니다."; //string.format
    }
    public override void ApplyEffect()
    {
        BattleManager.Inst.TargetGetDamage(null, target, _damagePerReinforce[reinforcedLevel]);
        // target.ownBuffs.Add(new Vulnerable());
    }
}
