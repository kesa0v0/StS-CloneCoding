using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBuff : BuffData
{
    public SampleBuff()
    {
        hasLifespan = false;
        isPerTurn = true;
        amount = 0;
        lifespan = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_12");
    }

    protected override void StaticEffect()
    {
        base.StaticEffect();

    }

    protected override void ActiveEffect()
    {
        base.ActiveEffect();
        
        Debug.Log("TestBuffActivation");
    }
}
