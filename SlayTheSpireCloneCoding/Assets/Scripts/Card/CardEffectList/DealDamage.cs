using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DealDamage : CardData
{
    public DealDamage() {
        cardName = "공격";
        energy = 1;
        reinforcedLevel = 0;
        sprite = getSpriteFromResources("Sprites/Characters/character_3");
    }

    private int[] _damagePerReinforce = new int[] { 5, 8 };


    public string CardDescription(int amount)
    {
        return $"대상에게 {_damagePerReinforce[reinforcedLevel]} 데미지를 가합니다"; //string.format
    }

    public void ApplyEffect(CharacterEntity target)
    {
        BattleManager.Inst.TargetGetDamage(null, target, _damagePerReinforce[reinforcedLevel]);
    }
}
