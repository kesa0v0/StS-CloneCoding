using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct EffectVariables
{
    public Effect effect;

    [Range(0, 2)]
    public int ReinforcedLevel;
}

[Serializable]
public class CardData
{
    public string name;
    public int energy;
    public Sprite sprite;


    

    public List<EffectVariables> effects = new List<EffectVariables>();


    public void ApplyEffect(CharacterEntity target){
        foreach (EffectVariables effectVar in effects)
        {
            effectVar.effect.target = target;
            effectVar.effect.ReinforcedLevel = effectVar.ReinforcedLevel;

            Debug.Log(effectVar.effect.CardDescription());
            effectVar.effect?.ApplyEffect();
        }
    }
}

[CreateAssetMenu(fileName ="CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public CardData[] cardDatas;
}