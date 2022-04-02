using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField] CharacterEntityData chEntityData;
    [SerializeField] GameObject entityBase;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] TMP_Text shieldTMP;
    [SerializeField] TMP_Text sanityTMP;
    [SerializeField] TMP_Text nameTMP;

    public int maxHealth;
    public int health;
    public int shield;
    public bool isUseSanity;
    public int maxSanity;
    public int sanity;
    public Vector3 originPos;

    public void Setup(CharacterEntityData entityData)
    {
        name = entityData.name;
        maxHealth = entityData.maxHealth;
        health = entityData.maxHealth;
        shield = entityData.initShield;
        maxSanity = entityData.maxSanity;
        sanity = entityData.maxSanity;
        isUseSanity = entityData.isUseSanity;

        this.chEntityData = entityData;
        UpdateTMP();
    }

    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(pos, dotweenTime);
        }
        else
        {
            transform.position = pos;
        }
    }

    public void UpdateTMP()
    {
        if (nameTMP != null)
            nameTMP.text = this.chEntityData.name;
        
        character.sprite = this.chEntityData.sprite;
        healthTMP.text = $"{health.ToString()} / {maxHealth.ToString()}";
        shieldTMP.text = shield.ToString();
        sanityTMP.text = $"{sanity.ToString()} / {maxSanity.ToString()}";

    }
}
