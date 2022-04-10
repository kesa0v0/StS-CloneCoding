using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffData
{
    [SerializeField] Buff AttatchedBuff;
    [SerializeField] CharacterEntity AttatchedEntity;
    [SerializeField] string name;
    public Sprite sprite;

    public bool isPerTurn;
    public bool hasLifespan;

    public int amount;   
    public int lifespan;

    public void Setup(Buff buff)
    {
        this.AttatchedBuff = buff;
        Debug.Log(buff.transform.gameObject.name);
        // Debug.Log(buff.transform.parent.gameObject.name);
        // this.AttatchedEntity = buff.transform.parent.gameObject.GetComponent<CharacterEntity>();
    }

    protected virtual void StaticEffect()
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
                AttatchedBuff.DestroyBuff();
            }
        }
    }

    public void MergeBuff() // 버프 합산 TODO:
    {

    }
}
