using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSample : BuffData
{
    public BuffSample()
    {
        hasLifespan = false;
        isPerTurn = true;
        varNum = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_12");
    }

    protected override void StaticEffectOn()
    {
        base.StaticEffectOn();

    }

    protected override void StaticEffectOff()
    {
        base.StaticEffectOff();

    }

    protected override void ActiveEffect()
    {
        base.ActiveEffect();
        
        Debug.Log("TestBuffActivation");
    }
}
