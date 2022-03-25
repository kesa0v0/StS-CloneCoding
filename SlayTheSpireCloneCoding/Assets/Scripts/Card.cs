using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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
    public PRS originPRS;

    public void Setup(CardData cardData, bool isfront = true)
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

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    private void OnMouseOver() {
        CardManager.Inst.CardMouseOver(this);
    }
    
    private void OnMouseExit() {
        CardManager.Inst.CardMouseExit(this);
    }

    private void OnMouseDown() {
        CardManager.Inst.CardMouseDown();
    }

    private void OnMouseUp() {
        CardManager.Inst.CardMouseUp();
    }
}
