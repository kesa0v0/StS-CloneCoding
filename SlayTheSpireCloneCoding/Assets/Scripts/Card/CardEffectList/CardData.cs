using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardData
{
    public string cardName;
    public int energy;
    public int reinforcedLevel;
    public Sprite sprite;

    public virtual void ApplyEffect()
    {

    }

    public Sprite getSpriteFromResources(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}