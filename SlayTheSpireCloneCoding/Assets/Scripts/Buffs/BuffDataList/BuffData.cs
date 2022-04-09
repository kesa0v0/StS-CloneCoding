using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData
{
    
    public int amount;
    public bool hasLifespan;
    public int lifespan;
    public Sprite sprite;
    

    #region StaticEffect
    private void Awake() { // 스탯 버프
        
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
