using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


[System.Serializable]
public class Buff : MonoBehaviour
{
    [SerializeField] SpriteRenderer BuffSprite;
    [SerializeField] TMP_Text BuffCountTMP;

    public BuffData buffData;

    public void Setup(BuffData buffData)
    {
        this.buffData = buffData;
        this.buffData.Setup(this);
        UpdateTMP();
    }

    public void UpdateTMP()
    {
        // this.BuffSprite.sprite = buffData.sprite;  //TODO: 버프 스프라이트 제작 or 크기 고정
        this.BuffCountTMP.text = buffData.varNum.ToString();
    }

    public void DestroyBuff()
    {
        this.transform.parent.gameObject.GetComponent<CharacterEntity>()?.ownBuffs.Remove(this);
        this.transform.gameObject.transform.DOKill();
        Destroy(this.transform.gameObject);
    }

    public void ActivateBuff() // TODO: 이거 더 적절한 이름 없으려나? 버프 활성화 하기
    {
        this.buffData.OnEndOfTurn();
    }
}