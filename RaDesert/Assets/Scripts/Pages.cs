using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{
    public GameObject cardPrefab;
    public CollectionCard[] cardsInThisPage;
    public Vector2 initialPosition;
    public Vector2 spawnPosition;
    public int rowNumber;
    public int columnNumber;
    public int cardIndex;
    public Vector2 offset;


    public void Start()
    {
        spawnPosition = initialPosition;
        CreateCardsInPage();
    }

    void CreateCardsInPage()
    {
        for (int i = 0; i < rowNumber; i++)
        {
            for(int j = 0; j < columnNumber; j++)
            {
                if(cardIndex < cardsInThisPage.Length)
                {
                    GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity, transform);
                    newCard.GetComponent<Card>().cardData = cardsInThisPage[cardIndex];
                    newCard.GetComponent<RectTransform>().anchoredPosition = spawnPosition;

                    cardIndex++;
                    spawnPosition.x += offset.x;
                }
            }

            spawnPosition.y += offset.y;
            spawnPosition.x = initialPosition.x;
        }
    }
}
