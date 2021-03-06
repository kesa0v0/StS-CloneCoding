using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffData
{
    public Buff attatchedBuff;
    public CharacterEntity attatchedEntity;
    public string name;
    public Sprite sprite;

    public bool isPerTurn;
    public bool hasLifespan;

    public int varNum;

    public void Setup(Buff buff)
    {
        this.attatchedBuff = buff;
        this.attatchedEntity = buff.transform.parent.parent.gameObject.GetComponent<CharacterEntity>();

        StaticEffectOn();
        StaticEffectUpdate();
        buff.UpdateTMP();
    }

    protected virtual void StaticEffectOn()
    {
        
    }

    protected virtual void StaticEffectUpdate()
    {
        
    }

    protected virtual void StaticEffectOff()
    {
        
    }

    protected virtual void ActiveEffect() // 효과 발동시 실행되는 버프
    {

    }

    public void OnEndOfTurn() // 턴마다 효과가 있는 버프
    {
        if (isPerTurn)
            ActiveEffect();
        
        if (hasLifespan)
        {
            varNum--;

            if (varNum <= 0)
            {
                StaticEffectOff();
                attatchedBuff.DestroyBuff();
            }
        }
        StaticEffectUpdate();
        attatchedBuff.UpdateTMP();
    }

    public void MergeBuff(BuffData buffData) // 버프 합산 TODO:
    {
        varNum += buffData.varNum;
    }
}
