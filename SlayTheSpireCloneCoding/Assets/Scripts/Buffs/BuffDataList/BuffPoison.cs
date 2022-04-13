using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPoison : BuffData
{
    public BuffPoison(){
        hasLifespan = true;
        isPerTurn = true;
        varNum = 2;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_12");
    }

    protected override void ActiveEffect()
    {
        base.ActiveEffect();

        BattleManager.Inst.TargetGetDamage(null, attatchedEntity, varNum);
    }
}
