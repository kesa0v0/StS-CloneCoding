using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPoison : CardData
{
    public CardPoison() {
        cardName = "중독";
        energy = 2;
        reinforcedLevel = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_5"); //TODO: 리소스 이거 성능때문에 쓰면 안된다는 이야기도 있던데 ㄹㅇ인가
    }

    int[] _poisonPerReinforce = new int[] { 8, 12 };

    public override string CardDescription()
    {
        base.CardDescription();
        return $"대상에게 { "중독" }을 { _poisonPerReinforce[reinforcedLevel] } 부여합니다."; //string.format
    }
    public override void ApplyEffect()
    {
        base.ApplyEffect();
        BattleManager.Inst.AddBuffToTarget(target, new BuffPoison());
    }
}
