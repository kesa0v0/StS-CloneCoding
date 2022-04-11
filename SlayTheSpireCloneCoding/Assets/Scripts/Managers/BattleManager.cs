using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject buffPrefab;
    [SerializeField] TMP_Text EnergyCounter;

    public CharacterEntity player;

    public int maxEnergy;
    public int currEnergy;

    private void Start() {
        TurnManager.OnTurnStarted += OnTurnStarted; // 턴 시작시 이벤트 반응 추가
    }

    private void OnDestroy() {
        TurnManager.OnTurnStarted -= OnTurnStarted; // 턴 시작시 이벤트 반응 제거
    }

    void OnTurnStarted(bool isMyTurn)
    {
        if (isMyTurn)
        {
            player.ResetShield(); // 내 턴에 내 쉴드 까기
            FillEnergy(); // 내 턴에 에너지 리필
            player.ActivateBuff(); // 내 턴에 내 액티브 버프 효과 발동
        }
        else
        {
            foreach (CharacterEntity enemy in EnemyManager.Inst.enemyList)
            {
                enemy.ResetShield(); // 적 턴에 적들 쉴드 까기
                enemy.ActivateBuff(); // 적 턴에 적 액티브 버프 효과 발동
            }
        }
    }

    #region Energy

    void updateEnergyCounter()
    {
        EnergyCounter.text = $"{currEnergy}/{maxEnergy}";
    }


    public void FillEnergy(bool toMax = true, int amount = 0)
    {
        if (toMax)
            currEnergy = maxEnergy;
        else
            currEnergy += amount;
        
        updateEnergyCounter();
    }

    public bool useEnergy(int amount)
    {
        if (currEnergy - amount < 0)
        {
            return false;
        }
        else
        {
            currEnergy -= amount;
            updateEnergyCounter();

            return true;
        }
    }

    #endregion

    #region Buff
    public void AddBuffToTarget(CharacterEntity target, BuffData buffData)
    {
        if (target == null)
            return;

        GameObject buffObject = Instantiate(buffPrefab, target.buffLocation.position, Utils.QI);
        buffObject.transform.parent = target.transform.Find("BuffList");
        Buff buff = buffObject.GetComponent<Buff>();
        buffObject.name = ("buff " + Random.Range(0, 1000).ToString());
        buff.Setup(buffData);

        target.ownBuffs.Add(buff);
        target.BuffAlignment();
    }

    #endregion

    #region Effects

    public void TargetGetDamage(CharacterEntity origin, CharacterEntity target, int amount) // 데미지 받는 함-수
    {
        float famount =
                (amount + origin.power) // 힘 더하기
                * ((100 + target.vulnerablePerc) / 100f) // 취약 퍼센트
                ;
        amount = (int) famount;

        int temp = target.shield - amount;
        if (temp > 0)
        {
            target.shield = temp;
            print($"쉴드뎀: {amount}"); // TODO: amount 플로팅 텍스트(쉴드로 방어한 데미지)
        }
        else if (temp == 0)
        {
            target.shield = 0;
            print("방어함!");// TODO: 방어함! 플로팅 텍스트
        }
        else
        {
            target.shield = 0;
            target.health += temp;
           print($"쉴드뎀: { target.shield }, 체력뎀: { target.shield + temp }"); // TODO: abs(temp), health+temp 플로팅 텍스트(쉴드 방어 데미지 + 체력에 받은 데미지)
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

    #endregion
}
