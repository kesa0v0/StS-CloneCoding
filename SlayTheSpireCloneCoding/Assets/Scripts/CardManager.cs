using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] CardSO cardSO;

    List<CardData> cardDeck;

    public CardData PopItem() // 카드뽑기
    {
        if (cardDeck.Count == 0) // 덱에 아무 카드도 없을시 덱 재생성
            SetupCardDeck();

        CardData cardData = cardDeck[0];
        cardDeck.RemoveAt(0);
        return cardData; 
    }

    void SetupCardDeck()
    {
        cardDeck = new List<CardData>();
        // TODO: 현재는 존재하는 카드 리스트에서 카드 뽑아옴. 덱-> 사용가능덱->손패->버림덱으로 바꿀 것.
        for (int i = 0; i < cardSO.cardDatas.Length; i++)
        { // 덱에 카드 넣기 
            cardDeck.Add(cardSO.cardDatas[i]);
        }

        for (int i = 0; i < cardDeck.Count; i++)
        { // 카드 셔플 TODO: 진짜 셔플하는거 맞는지 확인좀
            int rand = Random.Range(i, cardDeck.Count);
            CardData temp = cardDeck[i];
            cardDeck[i] = cardDeck[rand];
            cardDeck[rand] = temp;

        }
    }

    void Start()
    {
        SetupCardDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) // 디버그용 치트: 덱에서 뽑기
            print(PopItem().name);
    }
}
