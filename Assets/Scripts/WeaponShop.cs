using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponShop : MonoBehaviour
{
    public GameObject player;
    public GameObject shopCanvas;
    public int Cost = 50;
    public int weaponLvl = 1;
    public TextMeshProUGUI amount2pay;
    public TextMeshProUGUI fromLvl;
    public TextMeshProUGUI toLvl;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public void Update()
    {
        amount2pay.text = (Cost * (weaponLvl)).ToString();
        fromLvl.text = weaponLvl.ToString();
        toLvl.text = (weaponLvl + 1).ToString();
    }

    public void ShopOpen(bool WeaponShopIsOpen)
    {
        if (WeaponShopIsOpen == false)
        {
            shopCanvas.SetActive(true);
            WeaponShopIsOpen = true;
        }
        else if(WeaponShopIsOpen == true)
        {
            shopCanvas.SetActive(false);
            WeaponShopIsOpen = false;
        }
    }

    public void UpgradeWeapon()
    {
        player.GetComponent<Player>().UpgradeWeapon(Cost);
        weaponLvl++;
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }
}
