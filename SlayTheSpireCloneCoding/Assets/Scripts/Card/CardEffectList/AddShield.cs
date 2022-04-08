using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AddShield : CardData
{
    int[] _shieldPerLevel = new int[] { 5, 8 };

    public string CardDescription()
    {
        return $"대상에게 방어력 { _shieldPerLevel[reinforcedLevel] } 을 제공합니다"; //string.format
    }
    public void ApplyEffect(CharacterEntity target)
    {
        target.shield += _shieldPerLevel[reinforcedLevel]; //TODO: 이거 BattleManager로 옮기기ㅣ
    }
}
