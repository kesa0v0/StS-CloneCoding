using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField] SpriteRenderer character;

    [SerializeField] TMP_Text healthTMP;
    [SerializeField] TMP_Text shieldTMP;
    [SerializeField] TMP_Text sanityTMP;
    [SerializeField] TMP_Text nameTMP;


    public Transform buffLocation;
    public Vector3 originPos;
    public CharacterEntityData chEntityData;

    [Header("상태창")]
    // Status
    public int maxHealth;
    public int health;
    public int shield;
    public bool isUseSanity;
    public int maxSanity;
    public int sanity;
    public List<Buff> ownBuffs;

    [Header("버프 상태창")]
    // Status for Buff?
    public bool isResetShield = false; // 턴 시작때 쉴드 까는가 OX
    public int vulnerablePerc = 0;
    public int power = 0;


    public void Setup(CharacterEntityData entityData)
    {
        name = entityData.name;
        maxHealth = entityData.maxHealth;
        health = entityData.maxHealth;
        shield = entityData.initShield;
        maxSanity = entityData.maxSanity;
        sanity = entityData.maxSanity;
        isUseSanity = entityData.isUseSanity;

        ownBuffs = new List<Buff>();

        this.chEntityData = entityData;
        UpdateTMP();
    }

    #region UI

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

    public void KillMyself()
    {
        EnemyManager.Inst.RemoveEnemy(this);  // TODO: 현재는 적만 죽이는 상태인데, 내가 죽었을때는 게임 오버되도록 고쳐야 할듯ㄷ
        this.transform.gameObject.transform.DOKill();
        Destroy(this.transform.gameObject);
    }

    public void BuffAlignment()
    {
        for (int i = 0; i < ownBuffs.Count; i++)
        {
            Buff buff = ownBuffs[i];
            Vector3 tempVec = new Vector3((i % 5) * 1.25f, -(i / 5) * 1.25f, 0);
            buff.transform.DOLocalMove(tempVec, 0.05f);
        }
    }

    #endregion

    public bool IsAlive()
    {
        return health > 0 ? true : false;
    }


    public void ActivateBuff() // TODO: 이거 더 적절한 이름 없으려나? 버프 활성화 하기
    {
        foreach (Buff buff in ownBuffs)
        {
            buff.ActivateBuff();
        }
    }

    public void ResetShield(int amount = -1) // 쉴드 까는 스크립트
    {
        if (!isResetShield)
        {
            if (amount >= 0) // 이게 필요할진 모르겠지만, 일정 부분만 까는 스크립트
            {
                this.shield -= amount;
                UpdateTMP();
            }
            else
            {
                this.shield = 0;
                UpdateTMP();
            }
        }
    }


}
