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

    public int amount;   
    public int lifespan;

    public void Setup(Buff buff)
    {
        this.attatchedBuff = buff;
        this.attatchedEntity = buff.transform.parent.parent.gameObject.GetComponent<CharacterEntity>();

        StaticEffectOn();
    }

    protected virtual void StaticEffectOn()
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
            lifespan--;

            if (lifespan <= 0)
            {
                StaticEffectOff();
                attatchedBuff.DestroyBuff();
            }
        }
    }

    public void MergeBuff() // 버프 합산 TODO:
    {

    }
}
