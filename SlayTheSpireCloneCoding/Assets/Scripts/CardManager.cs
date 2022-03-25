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
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform handCardLeft;
    [SerializeField] Transform handCardRight;

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
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopItem());

        handCard.Add(card);

        SetOriginOrder();
        CardAlignment();
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

    void CardAlignment() // 카드 정렬
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(handCardLeft, handCardRight, handCard.Count, 0.5f, Vector3.one * 1.9f);

        var targetCards = handCard;
        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.3f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale) // 손패 동그랗게 만들기
    {
        float[] objLerps = new float[objCount]; // 0~1에서 자기 포지션 어딘지
        List<PRS> results = new List<PRS>(objCount); // objCount만큼 크기제한

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break; // 손패 1개일때
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break; // 손패 2개일 때
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break; // 손패 3개일 때
            default:
                float interval = 1f / (objCount - 1);  // 4개 이상일 때
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]); // objLerp에 따라 lefttr ~ righttr사이 위치값 정해줌
            var targetRot = Utils.QI;
            if (objCount >= 4) // 4개 이상부터는 회전 적용
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));  // 원의 방정식 이용. height == radius
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }
}
