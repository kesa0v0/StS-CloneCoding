using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardData : MonoBehaviour
{
    public string name;
    public int energy;
    public int reinforcedLevel;
    public Sprite sprite;

    public virtual void ApplyEffect()
    {

    }
}