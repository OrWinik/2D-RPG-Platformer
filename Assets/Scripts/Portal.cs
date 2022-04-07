using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "MainPlayer" && Input.GetKeyDown(KeyCode.UpArrow) && this.gameObject.tag == "ForwardPortal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (collision.tag == "MainPlayer" && Input.GetKeyDown(KeyCode.UpArrow) && this.gameObject.tag == "BackPortal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}

