using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardData // TODO: 지금은 몰라도 Effect랑 카드데이터랑 분리해야할지도 모름
{
    public string cardName;
    public int energy;
    public int reinforcedLevel;
    public Sprite sprite;

    public virtual void ApplyEffect()
    {

    }
}