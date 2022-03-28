using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public string name;
    public int energy;
    public Sprite sprite;

    public int[] Effects; // TODO: int -> effects

    public void ApplyEffect(CharacterEntity target){
        Debug.Log("EFFECT TEST");
    }
}

[CreateAssetMenu(fileName ="CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public CardData[] cardDatas;
}