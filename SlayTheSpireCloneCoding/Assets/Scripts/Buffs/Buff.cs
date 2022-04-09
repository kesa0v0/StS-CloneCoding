using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class Buff
{
    [SerializeField] SpriteRenderer BuffSprite;
    [SerializeField] TMP_Text BuffCountTMP;
    public BuffData buffData;


    public void Setup(BuffData buffData)
    {
        this.buffData = buffData;
        UpdateTMP();
    }

    public void UpdateTMP()
    {
        this.BuffSprite.sprite = buffData.sprite;
        this.BuffCountTMP.text = buffData.amount.ToString();
    }
}