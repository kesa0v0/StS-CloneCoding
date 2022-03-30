using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 치트, UI, 랭킹, 게임오버
public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] NotificationPanel notificationPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }


    [SerializeField] EntitySO enemySO;

    void InputCheatKey() // 디버그용 치트
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            TurnManager.OnAddCard?.Invoke();

        if (Input.GetKeyDown(KeyCode.Keypad2))
            TurnManager.Inst.EndTurn();

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (CardManager.Inst.getSelectCard())
                CardManager.Inst.DestroyCard(CardManager.Inst.getSelectCard());
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            EnemyManager.Inst.SpawnEnemy(enemySO.enemyDatas[0]);
        }
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.StartGameCo());
    }

    public void Notificaiton(string message)
    {
        notificationPanel.Show(message);
    }
}
