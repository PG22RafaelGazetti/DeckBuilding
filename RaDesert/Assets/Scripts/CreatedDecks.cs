using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreatedDecks : MonoBehaviour
{
    public GameObject[] createdDecks;
    public int totalDeckNumber = 4;
    public TextMeshProUGUI errorMessage;
    int currentNumberOfDecks = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        errorMessage.text = "";
        StopAllCoroutines();
    }

    public void CreateNewDeck()
    {
        if(currentNumberOfDecks < 4)
        {
            for (int i = 0; i < totalDeckNumber; i++)
            {
                if(!createdDecks[i].activeSelf)
                {
                    createdDecks[i].SetActive(true);
                    createdDecks[i].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "0/12";
                    break;
                }
            }

            currentNumberOfDecks++;
        }
        else
        {
            errorMessage.text = "Max Amount of Decks Reached";
            StopAllCoroutines();
            StartCoroutine(RemoveErrorText());
        }
    }

    public void DeleteDeck()
    { 
        currentNumberOfDecks--;
    }

    IEnumerator RemoveErrorText()
    {
        yield return new WaitForSeconds(2);

        errorMessage.text = "";
    }
}
