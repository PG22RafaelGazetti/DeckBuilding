using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public int numberOfPages;
    public int currentPage = 0;

    private void Start()
    {
        leftArrow.SetActive(false);
    }

    public void ChangePage(int valor)
    {
        currentPage += valor;

        for (int i = 0; i <= numberOfPages; i++)
        {
            if(i == currentPage)
            {
                pages[i].SetActive(true);
                continue;
            }

            pages[i].SetActive(false);
        }


        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        if(currentPage == 0)
        {
            leftArrow.SetActive(false);
        }

        if(currentPage == 3)
        {
            rightArrow.SetActive(false);
        }
    }
}
