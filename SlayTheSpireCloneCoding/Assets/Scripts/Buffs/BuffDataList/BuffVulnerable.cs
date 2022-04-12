using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffVulnerable : BuffData
{
    public BuffVulnerable()
    {
        hasLifespan = true;
        isPerTurn = false;
        varNum = 2;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_12");
    }

    protected override void StaticEffectOn()
    {
        base.StaticEffectOn();

        attatchedEntity.vulnerablePerc = 40;
    }

    protected override void StaticEffectOff()
    {
        base.StaticEffectOff();

        attatchedEntity.vulnerablePerc = 0;
    }
}
