using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject cardPrefab; // 카드 프리팹
    [SerializeField] Transform availableDeckPos; //
    [SerializeField] Transform discardDeckPos; //
    [SerializeField] Transform handDeckLeft;
    [SerializeField] Transform handDeckRight;
    [SerializeField] GameObject TargetIndicator;
    [SerializeField] ECardState eCardState;
    [SerializeField] CharacterEntity player;

    public List<CardData> allCardDeck; // 전체 카드 덱   // cardso
    [SerializeField] List<Card> availableDeck; // TODO: Card-> CardData로 바꿀수도 있워오
    [SerializeField] List<Card> handDeck; // 손에 들고있는 카드들 리스트
    [SerializeField] List<Card> discardedDeck;
    
    public int pickupCardNum;

    Card selectCard;
    CharacterEntity targetEntity;
    bool ExistTargetIndicatorEntity => targetEntity != null;
    bool isMyCardDrag;
    bool onMyCardArea;
    enum ECardState { Nothing, CanMouseOver, CanMouseDrag }


    #region Deck
    void SetupAvailableDeck() // 게임 시작시 카드 덱 제작
    {
        availableDeck = new List<Card>();
        for (int i = 0; i < allCardDeck.Count; i++)
        { // 덱에 카드 넣기 '
            availableDeck.Add(MakeCard(allCardDeck[i]));
        }

        ShuffleDeck(availableDeck);
    }

    Card MakeCard(CardData cardData) // 카드 Instantiate
    { 
        cardData.player = this.player;
        var cardObject = Instantiate(cardPrefab, availableDeckPos.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(cardData);
        card.makeVisible(false);

        cardObject.name = ("card " + card.cardData.cardName + Random.Range(0, 1000).ToString());
        return card;
    }

    List<Card> ShuffleDeck(List<Card> deck) // 덱 셔플
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = temp;
        }
        return deck;
    }

    void ResetAvailableDeck() // discardDeck 에서 AvailableDeck으로 넘기기
    {
        int leftCardNum = discardedDeck.Count;
        for (int i = 0; i < leftCardNum; i++)
        {
            Card card = discardedDeck[0];
            availableDeck.Add(card);
            discardedDeck.RemoveAt(0);
            card.MoveTransform(new PRS(availableDeckPos.position, Utils.QI, card.originPRS.scale), false);
        }

        ShuffleDeck(availableDeck);
    }

    void addTohandDeck() // 카드뽑기
    {
        if (availableDeck.Count == 0) // 덱에 아무 카드도 없을시 덱 재생성
        {
            if (discardedDeck.Count == 0)
                return;

            ResetAvailableDeck();
        }

        Card card = availableDeck[0];
        handDeck.Add(card);
        card.makeVisible(true);
        availableDeck.RemoveAt(0);

        SetOriginOrder();
        CardAlignment();
    }

    void PickupCards(int pickNum)
    {
        for (int i=0; i<pickNum; i++)
        {
            addTohandDeck();
        }
    }

    public IEnumerator DiscardCard(Card card)
    {
        discardedDeck.Add(card);
        handDeck.Remove(card);

        card.MoveTransform(new PRS(discardDeckPos.position, Utils.QI, card.originPRS.scale), true, 0.1f);
        yield return new WaitForSeconds(0.1f); // TODO: 나중에 고칩시다 ㄹㅇㅋㅋ (카드 옮기고 지우는거 따로) < coroutine (정렬하는거 따로)
        
        card.makeVisible(false);

        SetOriginOrder();
        CardAlignment();
    }

    #endregion

    void Start() // 시작할 때 덱 셋업
    {

        allCardDeck = new List<CardData>() { // 샘플 전체덱 TODO: 지우기
            new AddShield(),
            new AddShield(),        
            new AddShield(),        
            new DealDamage(),
            new DealDamage(),
            new DealDamage(),
            new Smite(),
            new Power()
        };


        SetupAvailableDeck();
        TurnManager.OnAddCard += PickupCards; // 카드추가 이벤트 반응 추가
        TurnManager.OnTurnStarted += OnTurnStarted; // 턴 시작시 이벤트 반응 추가
    }

    void OnDestroy()
    {
        TurnManager.OnAddCard -= PickupCards; // 카드추가 이벤트 반응 제거
        TurnManager.OnTurnStarted -= OnTurnStarted; // 턴 시작시 이벤트 반응 제거
    }

    void Update()
    {
        if (isMyCardDrag)
            CardDrag();

        SetECardState();
        DetectCardArea();
        ShowTargetIndicator(ExistTargetIndicatorEntity);
    }
    
    void OnTurnStarted(bool isMyTurn)
    {
        if (isMyTurn)
        {
            PickupCards(pickupCardNum); // 내 턴에 손 카드 받기
        }
    }

    public IEnumerator EndTurnCards()
    {
        // 남은 손 카드 버리기
        int leftCardNum = handDeck.Count; 
        for (int i = 0; i < leftCardNum; i++)
        {
            yield return DiscardCard(handDeck[0]);
        }
    }


    #region CardAlign
    void SetOriginOrder() // 카드 오더 정하는 것
    {
        int count = handDeck.Count;
        for (int i = 0; i < count; i++) // TODO: 이거 카드 추가할 때 마다 반복문 돌려서 count 높아질수록 리소스 많이 잡아먹을꺼같은데 더 좋은 방법 있을까
        {
            var targetCard = handDeck[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }
    void CardAlignment() // 카드 정렬
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(handDeckLeft, handDeckRight, handDeck.Count, 0.5f, Vector3.one * 1.9f);

        var targetCards = handDeck;
        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.9f);
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

    #endregion

    #region CardMouseControl
    public void CardMouseOver(Card card)
    {
        if (eCardState == ECardState.Nothing)
            return;
        
        if (!isMyCardDrag)
        {
            selectCard = card;
            EnlargeCard(true, card);
        }
    }

    public void CardMouseExit(Card card)
    {
        if (!isMyCardDrag)
            EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
    }

    public void CardMouseUp()
    {
        isMyCardDrag = false;
        EnlargeCard(false, selectCard);

        if (eCardState != ECardState.CanMouseDrag)
            return;
        
        if (selectCard && targetEntity)
            UseCardEffect(selectCard, targetEntity);
        
        selectCard = null;
        targetEntity = null;
    }

    public void CardMouseDrag() //마우스 드래그 
    {
        if (!(TurnManager.Inst.isMyTurn && !TurnManager.Inst.isLoading) || selectCard == null)
            return;

        GetTargetEntity();
    }

    void GetTargetEntity()
    {
        bool existTarget = false;
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward, Mathf.Infinity);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.layer == 7)
            {
                CharacterEntity cEntity = hit.collider?.gameObject.transform.parent.GetComponent<CharacterEntity>();
                if (cEntity != null)
                {
                    targetEntity = cEntity;
                    existTarget = true;
                    break;
                }
            }
        }
        if (!existTarget)
        targetEntity = null;
    }

    void CardDrag() // 카드 드래그
    {
        bool nowOnCardArea = DetectCardArea();

        if (onMyCardArea != nowOnCardArea)
        {
            // selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
            Vector3 DragPos = new Vector3((handDeckLeft.position.x + handDeckRight.position.x)/2, -9f, -10f);

            selectCard.MoveTransform(new PRS(DragPos, Utils.QI, selectCard.originPRS.scale * 1.5f), true, 0.5f);
            
        }

        onMyCardArea = nowOnCardArea;
    }

    bool DetectCardArea() // 마우스가 카드 영역에 있는지 확인
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        return Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    void EnlargeCard(bool isEnlarge, Card card) // 카드 확대
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -4f, -10f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 3.5f), false);
        }
        else
            card.MoveTransform(card.originPRS, true, 0.3f);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

    #endregion

    void SetECardState() //카드 움직임 가능성 상태머신
    {
        if (TurnManager.Inst.isLoading) // 로딩중일때
            eCardState = ECardState.Nothing; // 확대, 드래그 X

        else if (!TurnManager.Inst.isMyTurn) // 로딩 아닐때
            eCardState = ECardState.CanMouseOver; // 확대 O 드래그 X
        
        else if (TurnManager.Inst.isMyTurn) // 내 턴일때
            eCardState = ECardState.CanMouseDrag; // 확대, 드래그 O
    }

    public void DestroyCard(Card card)
    {
        handDeck.Remove(card);
        card.transform.DOKill();
        Destroy(card.gameObject);
        selectCard = null;
        CardAlignment();
    }

    //TODO: remove this debug
    public Card getSelectCard()
    {
        return selectCard;
    }

    void ShowTargetIndicator(bool isShow)
    {
        TargetIndicator.SetActive(isShow);
        if (ExistTargetIndicatorEntity)
            TargetIndicator.transform.position = targetEntity.transform.position;
    }

    void UseCardEffect(Card card, CharacterEntity target)
    {
        if (BattleManager.Inst.useEnergy(card.cardData.energy))
        {
            card.cardData.target = target;
            card.cardData.ApplyEffect();
            if (target != null)
            {
                target?.GetComponent<CharacterEntity>().UpdateTMP();
            }

            StartCoroutine(DiscardCard(card));
        }
        else
        {
            print("Not Enough Energy"); // TODO: 이거 화면에 띄우기
        }
    }

}
