using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 치트, UI, 랭킹, 게임오버
public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] NotificationPanel notificationPanel;

    void Start()
    {
        TurnManager.Inst.StartGame();
    }

    void Update()
    {
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }


    [SerializeField] EnemySO enemySO;

    void InputCheatKey() // 디버그용 치트
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) // 카드 1개 받기
            TurnManager.OnAddCard?.Invoke();

        if (Input.GetKeyDown(KeyCode.Keypad2)) // 턴 끝내기
            TurnManager.Inst.EndTurn();

        if (Input.GetKeyDown(KeyCode.Keypad3)) // 적 스폰
        {
            EnemyManager.Inst.SpawnEnemy(enemySO.enemyDatas[0]);
        }

        if (Input.GetKeyDown(KeyCode.Keypad4)) // 에너지 99 추가
        {
            BattleManager.Inst.FillEnergy(false, 99);
        }
    }

    public void Notificaiton(string message)
    {
        notificationPanel.Show(message);
    }
}
