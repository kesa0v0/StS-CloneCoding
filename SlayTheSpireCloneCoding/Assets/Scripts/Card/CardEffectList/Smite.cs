using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smite : CardData
{
    public Smite() {
        cardName = "강타";
        energy = 2;
        reinforcedLevel = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_5"); //TODO: 리소스 이거 성능때문에 쓰면 안된다는 이야기도 있던데 ㄹㅇ인가
    }

    int[] _damagePerReinforce = new int[] { 8, 12 };

    public override string CardDescription()
    {
        base.CardDescription();
        return $"대상에게 { _damagePerReinforce[reinforcedLevel] } 데미지를 가하고 { "취약" }을 { 2 } 부여합니다."; //string.format
    }
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        BattleManager.Inst.TargetGetDamage(null, target, _damagePerReinforce[reinforcedLevel]);
        BattleManager.Inst.AddBuffToTarget(target, new SampleBuff()); // TODO: 취약Vulnerable로 고치기
    }
}
