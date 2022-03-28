using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public string name;
    public int energy;
    public Sprite sprite;

    public List<Effect> effects = new List<Effect>(); // Effect, amount, target

    public void ApplyEffect(CharacterEntity target){
        foreach (Effect effect in effects)
        {
            Debug.Log(effect.CardDescription());
            effect?.ApplyEffect();
        }
    }
}

[CreateAssetMenu(fileName ="CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public CardData[] cardDatas;
}