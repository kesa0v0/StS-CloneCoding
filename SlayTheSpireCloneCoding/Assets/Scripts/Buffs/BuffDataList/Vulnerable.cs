using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : BuffData
{
    public Vulnerable()
    {
        amount = 0;
        hasLifespan = true;
        lifespan = 0;
        sprite = Resources.Load<Sprite>("Sprites/Characters/character_12");
    }

    
}
