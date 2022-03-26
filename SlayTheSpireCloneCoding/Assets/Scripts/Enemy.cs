using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject Base;
    [SerializeField] SpriteRenderer Character;
    [SerializeField] TMP_Text HealthTMP;
    [SerializeField] TMP_Text SanityTMP;
    [SerializeField] TMP_Text NameTMP;
    [SerializeField] TMP_Text IntendTMP;

    public int health;
    public int sanity;

    
    public void Setup()
    {
        
    }
}
