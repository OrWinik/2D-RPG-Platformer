using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private int coinsAmount;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "MainPlayer" && Input.GetKeyDown(KeyCode.Z))
        {
            coinsAmount = Random.Range(1, 5);
            Destroy(this.gameObject);
            FindObjectOfType<AudioManager>().play("Coins");
            collision.GetComponent<Player>().Coins(coinsAmount);
        }
    }
}
