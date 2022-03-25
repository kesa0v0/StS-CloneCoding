using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] CardSO cardSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> handCard;

    List<CardData> cardDeck;


    public CardData PopItem() // 카드뽑기
    {
        if (cardDeck.Count == 0) // 덱에 아무 카드도 없을시 덱 재생성
            SetupCardDeck();

        CardData cardData = cardDeck[0];
        cardDeck.RemoveAt(0);
        return cardData;
    }

    void SetupCardDeck() // 카드 덱 제작 및 셔플
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

    void Start() // 시작할 때 덱 셋업
    {
        SetupCardDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) // 디버그용 치트
            AddCard();
    }

    void AddCard() // 카드 Instantiate
    {
        var cardObject = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopItem());

        handCard.Add(card);

        SetOriginOrder();
    }

    void SetOriginOrder() // 카드 오더 정하는 것
    {
        int count = handCard.Count;
        for (int i = 0; i < count; i++) // TODO: 이거 카드 추가할 때 마다 반복문 돌려서 count 높아질수록 리소스 많이 잡아먹을꺼같은데
        {
            var targetCard = handCard[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

}
