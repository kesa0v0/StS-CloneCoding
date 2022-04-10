using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : BuffData
{
    public Vulnerable()
    {
        hasLifespan = true;
        isPerTurn = false;
        amount = 0;
        lifespan = 2;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_12");
    }

    protected override void StaticEffect()
    {
        base.StaticEffect();

    }

    protected override void ActiveEffect()
    {
        base.ActiveEffect();
        
    }
}
