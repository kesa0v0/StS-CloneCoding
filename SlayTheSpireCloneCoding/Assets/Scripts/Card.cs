using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text energyTMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    public CardData cardData;
    bool isfront;

    public void Setup(CardData cardData, bool isfront=true)
    {
        this.cardData = cardData;
        this.isfront = isfront;

        if (this.isfront)
        {
            character.sprite = this.cardData.sprite;
            nameTMP.text = this.cardData.name;
            energyTMP.text = this.cardData.energy.ToString();
        }
        else
        {
            card.sprite = cardBack;
            nameTMP.text = "";
            energyTMP.text = "";
        }
    }
}
