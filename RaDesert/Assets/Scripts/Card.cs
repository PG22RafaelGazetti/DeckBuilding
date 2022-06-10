using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public CollectionCard cardData;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardDescription;

    public TextMeshProUGUI cardAttack;

    public TextMeshProUGUI cardHealth;

    public TextMeshProUGUI cardCost;
    
    public TextMeshProUGUI cardType;

    void Start()
    {
        cardName.text = cardData.cardName;
        cardDescription.text = cardData.cardDescription;
        cardAttack.text = cardData.cardAttack.ToString();
        cardHealth.text = cardData.cardHealth.ToString();
        cardCost.text = cardData.cardCost.ToString();
        cardType.text = cardData.cardType.ToString();
        
    }
}
