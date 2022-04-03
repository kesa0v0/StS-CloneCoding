using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Inst { get; private set; }
    void Awake() => Inst = this;
    bool CanMouseInput => TurnManager.Inst.isMyTurn && !TurnManager.Inst.isLoading;

    WaitForSeconds delay1 = new WaitForSeconds(1);

    public void getDamage(CharacterEntity target, int amount) // 데미지 받는 함-수 (Manager로 옮길수도 있워오)
    {
        int temp = target.shield - amount;
        if (temp > 0)
        {
            target.shield = temp;
            print($"쉴드: {amount}"); // amount 플로팅 텍스트(쉴드로 방어한 데미지)
        }
        else if (temp == 0)
        {
            target.shield = 0;
            print("방어함!");// 방어함! 플로팅 텍스트
        }
        else
        {
            target.shield = 0;
            target.health += temp;
           print($"쉴드: { Mathf.Abs(temp) }, 체력뎀: { target.health+temp }"); // abs(temp), health+temp 플로팅 텍스트(쉴드 방어 데미지 + 체력에 받은 데미지)p
        }

        target.UpdateTMP();

        if (target.health <= 0) // 죽음 체크
        {
            print("죽음! 대상: " + this.name);
            if (target.chEntityData.GetType() == typeof(EnemyData))
            {
                print("대상은 Enemy입니다.");
                target.KillMyself();
            }
            
        }
    }
    
}
