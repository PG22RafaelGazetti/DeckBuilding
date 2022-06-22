using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckCard : MonoBehaviour
{
    public CollectionCard cardData;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardCost;

    public Image cardImage;

    public int positionInDeck;

    private Button button;

    private void Start()
    {
        GameObject deckObject = GameObject.FindWithTag("Deck");
        Deck deck = deckObject.GetComponent<Deck>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { deck.RemoveCardFromDeck(positionInDeck); });
    }

    public void UpdateCard(CollectionCard cardData)
    {
        this.cardData = cardData;
        cardName.text = cardData.cardName;
        cardCost.text = cardData.cardCost.ToString();
        cardImage.color = cardData.imageColor;
    }
}
