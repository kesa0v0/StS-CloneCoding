using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Inst { get; private set; }
    void Awake() => Inst = this;
    bool CanMouseInput => TurnManager.Inst.isMyTurn && !TurnManager.Inst.isLoading;

    WaitForSeconds delay1 = new WaitForSeconds(1);

}
