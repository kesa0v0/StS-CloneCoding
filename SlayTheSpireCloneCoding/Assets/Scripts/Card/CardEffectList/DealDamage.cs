using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DealDamage : CardData
{
    public string name = "공격"; //이렇게 하는게 맞는지는 몰?루
    public int energy = 1;
    public int reinforcedLevel = 0;
    public Sprite sprite;

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
