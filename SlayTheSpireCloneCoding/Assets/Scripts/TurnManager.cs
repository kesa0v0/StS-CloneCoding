using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField][Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTurnMode;
    [SerializeField][Tooltip("카드 배분이 매우 빨라집니다")] public bool fastMode;
    [SerializeField][Tooltip("시작 카드 개수를 정합니다")] int startCardCount;

    [Header("Properties")]
    public bool myTurn;
    public bool isLoading;

    enum ETurnMode { Random, My, Other }
    WaitForSeconds delay05 = new WaitForSeconds(0.5f); // 0.5초 기다리기?
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action OnAddCard; // 카드추가 이벤트


    void GameSetup()
    {
        if (fastMode)
            delay05 = new WaitForSeconds(0.05f);

        switch (eTurnMode)
        { // 누구 턴으로 시작할 것인지 정함 아마 항상 내턴일듯
            case ETurnMode.Random: // 랜덤
                myTurn = Random.Range(0, 2) == 0;
                break;
            case ETurnMode.My:
                myTurn = true;
                break;
            case ETurnMode.Other:
                myTurn = false;
                break;
        }
    }

    public IEnumerator StartGameCo()
    {
        GameSetup();

        isLoading = true;
        for (int i = 0; i < startCardCount; i++)
        {
            yield return delay05;
            OnAddCard?.Invoke();
        }
        StartCoroutine(StartTurnCo());
    }

    IEnumerator StartTurnCo()
    {
        isLoading = true;
        
        yield return delay07;
        OnAddCard?.Invoke(); // 무조건 나한테 카드 추가. FIXME: 내턴 아닐때 시작시 실행 안하도록 하기
        yield return delay07;
        isLoading = false;
    }
}
