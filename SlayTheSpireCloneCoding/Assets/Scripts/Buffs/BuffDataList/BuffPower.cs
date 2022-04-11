using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPower : BuffData
{
    public BuffPower(){
        hasLifespan = false;
        isPerTurn = false;
        amount = 0;
    }

    protected override void StaticEffectOn()
    {
        base.StaticEffectOn();

        attatchedEntity.power = 2;
    }

    protected override void StaticEffectOff()
    {
        base.StaticEffectOff();

        attatchedEntity.power = 0;
    }

    protected override void ActiveEffect()
    {
        base.ActiveEffect();
        
    }
}
