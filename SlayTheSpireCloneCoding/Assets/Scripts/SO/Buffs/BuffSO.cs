using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Buff
{
    public int amount;
    public int lifespan;

    public void nextTurn()
    {

    }

}

[CreateAssetMenu(fileName ="BuffSO", menuName = "Scriptable Object/BuffSO")]
public class BuffSO : ScriptableObject
{
    
}