using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PotionsShop : MonoBehaviour
{
    public GameObject ShopCanvas;
    public int PotionsBought = 0;
    public TextMeshProUGUI potions2buyText;
    private int potionCost = 20;
    public TextMeshProUGUI costText;
    public int amountToPay;

    private GameObject Player;

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public void Update()
    {
        potions2buyText.text = PotionsBought.ToString();
        costText.text = (potionCost * PotionsBought).ToString();
        amountToPay = potionCost * PotionsBought;
    }

    public void Shop(bool shopIsOpen)
    {
        if (shopIsOpen == false)
        {
            ShopCanvas.SetActive(true);
            shopIsOpen = true;
        }
        else if(shopIsOpen == true)
        {
            ShopCanvas.SetActive(false);
            shopIsOpen = false;
        }
            
    }

    public void CloseShop()
    {
        ShopCanvas.SetActive(false);
    }

    public void Buy()
    {
        Player.GetComponent<Player>().BuyingPotions(PotionsBought, amountToPay);
        ShopCanvas.SetActive(false);
    }

    public void AddPotion()
    {
        PotionsBought++;
    }

    public void RetractPotion()
    {
        PotionsBought--;
    }
}