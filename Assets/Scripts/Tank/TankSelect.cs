using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankSelect : MonoBehaviour
{
    public GameObject[] Changetanks;
    public int selectedTank;
    public Tanks[] tanks;
    public Button unlockButton;
    public Button useButton;
    public TextMeshProUGUI txtCoin;

    private void Awake()
    {
        selectedTank = PlayerPrefs.GetInt("SelectedTank", 0);
        foreach (GameObject tank in Changetanks)
        {
            tank.SetActive(false);
        }
        Changetanks[selectedTank].SetActive(true);
        foreach (Tanks t in tanks)
        {
            if (t.price == 0)
            {
                t.isUnlocked = true;
            }
            else
            {
                t.isUnlocked = PlayerPrefs.GetInt(t.name, 0) == 0 ? false : true;
            }
            UpdateUI();       
        }

    }
    public void ChangeNext()
    {
        Changetanks[selectedTank].SetActive(false);
        selectedTank++;
        if (selectedTank == Changetanks.Length)
        {
            selectedTank = 0;
        }
        Changetanks[selectedTank].SetActive(true);
        if (tanks[selectedTank].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedTank", selectedTank);
        }
        UpdateUI();
    }
    public void ChangePrevious()
    {
        Changetanks[selectedTank].SetActive(false);
        selectedTank--;
        if (selectedTank == -1)
        {
            selectedTank = Changetanks.Length - 1;
        }
        Changetanks[selectedTank].SetActive(true);
        if (tanks[selectedTank].isUnlocked)
        {
            PlayerPrefs.SetInt("SelectedTank", selectedTank);
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        txtCoin.text = "" + PlayerPrefs.GetInt("NumberOfCoins", 0);
        if (tanks[selectedTank].isUnlocked == true)
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price\t" + tanks[selectedTank].price;
            if (PlayerPrefs.GetInt("NumberOfCoins", 0) < tanks[selectedTank].price)
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = false;
            }
            else
            {
                unlockButton.gameObject.SetActive(true);
                unlockButton.interactable = true;
            }
        }
        if (tanks[selectedTank].isUnlocked == false)
        {
            useButton.gameObject.SetActive(false);
        }
        else
        {
            useButton.gameObject.SetActive(true);
        }
    }
    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        int price = tanks[selectedTank].price;
        PlayerPrefs.SetInt("NumberOfCoins", coins - price);
        PlayerPrefs.SetInt(tanks[selectedTank].name, 1);
        PlayerPrefs.SetInt("SelectedTank", selectedTank);
        tanks[selectedTank].isUnlocked = true;
        UpdateUI();
    }
}