using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : CardData
{
    public Power() {
        cardName = "힘";
        energy = 1;
        reinforcedLevel = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_6"); //TODO: 리소스 이거 성능때문에 쓰면 안된다는 이야기도 있던데 ㄹㅇ인가
    }

    int[] _PowerPerReinforce = new int[] { 2, 4 };

    public override string CardDescription()
    {
        base.CardDescription();
        return $"대상에게 { "힘" }을 { _PowerPerReinforce[reinforcedLevel] } 부여합니다."; //string.format
    }
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        BattleManager.Inst.AddBuffToTarget(target, new BuffPower()); // TODO: 취약Vulnerable로 고치기
    }
}
