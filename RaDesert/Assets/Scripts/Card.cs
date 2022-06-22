using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public GameObject deckVersion;

    public CollectionCard cardData;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardDescription;

    public TextMeshProUGUI cardAttack;

    public TextMeshProUGUI cardHealth;

    public TextMeshProUGUI cardCost;
    
    public TextMeshProUGUI cardType;

    public Image cardImage;

    private Button button;

    void Start()
    {
        GameObject deckObject = GameObject.FindWithTag("Deck");
        Deck deck = deckObject.GetComponent<Deck>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { deck.AddCardToDeck(gameObject); });
        gameObject.name = cardData.cardName;

        cardName.text = cardData.cardName;
        cardDescription.text = cardData.cardDescription;
        cardAttack.text = cardData.cardAttack.ToString();
        cardHealth.text = cardData.cardHealth.ToString();
        cardCost.text = cardData.cardCost.ToString();
        cardType.text = cardData.cardType.ToString();
        cardImage.color = cardData.imageColor;
    }

    public void SwitchToDeckMode()
    {
        deckVersion.SetActive(true);
        gameObject.SetActive(false);
    }
}
