using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPower : BuffData
{
    public BuffPower(){
        hasLifespan = false;
        isPerTurn = false;
        varNum = 2;
    }

    protected override void StaticEffectUpdate()
    {
        base.StaticEffectUpdate();

        attatchedEntity.power = varNum;
    }
}
