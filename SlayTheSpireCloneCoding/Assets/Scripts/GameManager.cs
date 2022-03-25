using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 치트, UI, 랭킹, 게임오버
public class GameManager : MonoBehaviour
{
    public static GameManager Inst {get; private set;}
    void Awake() => Inst = this;

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

    void InputCheatKey() // 디버그용 치트
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            TurnManager.OnAddCard?.Invoke();
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.StartGameCo());
    }
}
