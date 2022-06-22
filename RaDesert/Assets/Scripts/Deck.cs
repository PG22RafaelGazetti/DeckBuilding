using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Deck : MonoBehaviour
{
    public GameObject cardPrefab;
    public Button FinishDeckButtom;
    public Vector2[] cardsPosition;
    public int initialYPosition;
    public int offsetYIncrease;
    public int totalCards;
    public int currentNumberOfCards = 0;
    public GameObject[] deckCards;
    public GameObject[] cardsSelected;
    public TextMeshProUGUI totalCardsText;
    public TextMeshProUGUI homePageTotalCardsText;
    public TextMeshProUGUI errorText;
    public GameObject tab;


    private int lastAddedIndex;

    public void Start()
    {
        for (int i = 0; i < totalCards; i++)
        {
            cardsPosition[i].y = initialYPosition;
            GameObject tempCard = Instantiate(cardPrefab, cardsPosition[i], Quaternion.identity, transform);
            tempCard.GetComponent<RectTransform>().anchoredPosition = cardsPosition[i];
            tempCard.GetComponent<DeckCard>().positionInDeck = i;
            deckCards[i] = tempCard;
            initialYPosition += offsetYIncrease;
        }

        tab.SetActive(false);
    }

    private void OnEnable()
    {
        errorText.text = "";
        StopAllCoroutines();
    }

    public void Update()
    {
        totalCardsText.text = currentNumberOfCards.ToString() + "/ 12";
        homePageTotalCardsText.text = currentNumberOfCards.ToString() + "/ 12";

        if (currentNumberOfCards == 12)
        {
            FinishDeckButtom.interactable = true;
        }
        else
        {
            FinishDeckButtom.interactable = false;
        }
    }

    public void AddCardToDeck(GameObject cardSelected)
    {
        if(currentNumberOfCards < totalCards)
        {
            int numberOfCopies = 0;
            bool haveTwoCopies = false;

            for(int i = 0; i < deckCards.Length; i++)
            {
                if(cardsSelected[i] != null)
                {
                    if (cardSelected.name == cardsSelected[i].name)
                    {
                        Debug.Log(cardSelected);
                        Debug.Log(cardsSelected[i].name);
                        numberOfCopies++;

                        if (numberOfCopies == 2)
                        {
                            errorText.text = "You Can Only Have Two Copies of Each Card";
                            StopAllCoroutines();
                            StartCoroutine(RemoveErrorText());
                            haveTwoCopies = true;
                            break;
                        }
                    }
                }
            }

            if(!haveTwoCopies)
            {
                for (int i = 0; i < deckCards.Length; i++)
                {
                    if (deckCards[i].GetComponent<DeckCard>().cardData == null)
                    {
                        deckCards[i].GetComponent<DeckCard>().UpdateCard(cardSelected.GetComponent<Card>().cardData);
                        deckCards[i].SetActive(true);
                        cardsSelected[i] = cardSelected;
                        cardSelected.SetActive(false);
                        lastAddedIndex = i;
                        break;
                    }
                }
                currentNumberOfCards++;
            }
        }
        else
        {
            errorText.text = "Your Deck is Full";
            StopAllCoroutines();
            StartCoroutine(RemoveErrorText());
        }
    }

    public void RemoveCardFromDeck(int index)
    {
        deckCards[index].GetComponent<DeckCard>().cardData = null;
        deckCards[index].SetActive(false);
        cardsSelected[index].SetActive(true);
        cardsSelected[index] = null;
        currentNumberOfCards--;
    }

    public void ClearDeck()
    {
        for (int i = 0; i < deckCards.Length; i++)
        {
            deckCards[i].GetComponent<DeckCard>().cardData = null;
            deckCards[i].SetActive(false);

            if(cardsSelected[i] != null)
            {
                cardsSelected[i].SetActive(true);
                cardsSelected[i] = null;
            }

        }

        currentNumberOfCards = 0;
    }

    IEnumerator RemoveErrorText()
    {
        yield return new WaitForSeconds(2);

        errorText.text = "";
    }
}
