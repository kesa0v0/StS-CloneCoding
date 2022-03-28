using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Effect : ScriptableObject
{
    public CharacterEntity target;
    public int amount;
    public virtual string CardDescription()
    {
        return "";
    }

    public virtual void ApplyEffect()
    {
        
    }
}