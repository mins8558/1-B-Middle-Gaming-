using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent;
    public int pairCount;

    private Card firstCard;
    private Card secondCard;
    private bool isChecking = false;

    void Start()
    {
        GenerateCards();
    }

    void GenerateCards()
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < pairCount; i++)
        {
            numbers.Add(i);
            numbers.Add(i);
        }

        // 셔플
        for (int i = 0; i < numbers.Count; i++)
        {
            int temp = numbers[i];
            int randomIndex = Random.Range(i, numbers.Count);
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        foreach (int num in numbers)
        {
            GameObject newCard = Instantiate(cardPrefab, cardParent);
            Card cardScript = newCard.GetComponent<Card>();
            
            // ★ 중요: 번호를 설정합니다.
            cardScript.SetNumber(num); 
            newCard.name = "Card_ID_" + num; // 하이라키 창에서 번호 확인용
        }
    }

    public void CheckMatch(Card clickedCard)
    {
        if (isChecking || clickedCard == firstCard) return;

        if (firstCard == null)
        {
            firstCard = clickedCard;
            Debug.Log("첫 번째 카드 선택! 번호: " + firstCard.cardID);
        }
        else
        {
            secondCard = clickedCard;
            Debug.Log("두 번째 카드 선택! 번호: " + secondCard.cardID);
            StartCoroutine(CheckRoutine());
        }
    }

    IEnumerator CheckRoutine()
    {
        isChecking = true;

        if (firstCard.cardID == secondCard.cardID)
        {
            Debug.Log("<color=cyan>매칭 성공! 번호가 같습니다.</color>");
            firstCard = null;
            secondCard = null;
        }
        else
        {
            Debug.Log("<color=red>매칭 실패! 번호가 다릅니다.</color>");
            yield return new WaitForSeconds(0.8f);
            firstCard.CloseCard();
            secondCard.CloseCard();
            firstCard = null;
            secondCard = null;
        }

        isChecking = false;
    }
}