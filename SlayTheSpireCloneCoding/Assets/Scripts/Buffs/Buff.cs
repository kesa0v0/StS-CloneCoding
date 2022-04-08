using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Buff
{
    public int amount;
    public bool hasLifespan;
    public int lifespan;

    #region StaticEffect
    private void Awake(int lifespan = 3) { // 스탯 버프
        
    }

    private void OnDestroy() {
        
    }

    #endregion

    #region PerTurnEffect
    public void OnEndOfTurn() // 턴마다 효과가 있는 버프
    {
        PerTurnBuff();
        
        if (hasLifespan)
        {
            lifespan--;
            
            if (lifespan <= 0)
            {
                //destroy
            }
        }

    }

    void PerTurnBuff()
    {

    }
    #endregion
}