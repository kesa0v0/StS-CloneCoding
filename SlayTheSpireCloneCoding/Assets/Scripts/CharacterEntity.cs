using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField] CharacterEntityData chEntityData;
    [SerializeField] GameObject entityBase;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] TMP_Text sanityTMP;
    [SerializeField] TMP_Text nameTMP;

    public int health;
    public int sanity;
    public bool isUseSanity;
    
    public void Setup(CharacterEntityData entityData)
    {
        health = entityData.health;
        sanity = entityData.sanity;
        isUseSanity = entityData.isUseSanity;

        this.chEntityData = entityData;
        character.sprite = this.chEntityData.sprite;
        nameTMP.text = this.chEntityData.name;
        healthTMP.text = health.ToString();
        sanityTMP.text = sanity.ToString();
    }
}
