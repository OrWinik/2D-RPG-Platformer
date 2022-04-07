using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject player;
    public bool menuIsOn = false;
    public Transform pauseMenu;
    public GameObject deathMenu;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        if(player != null)
        {
            pauseMenu = player.transform.GetChild(3).gameObject.GetComponent<Transform>();

        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void Resume()
    {
        if(menuIsOn == false)
        {
            menuIsOn = true;
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if(menuIsOn == true)
        {
            menuIsOn = false;
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ResumeButton()
    {
        menuIsOn = false;
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        //player.GetComponent<Player>().SaveButton();
        deathMenu.SetActive(false);
        player.transform.position = new Vector3(23, -1, 0);
    }

    public void RespawnButton()
    {
        SceneManager.LoadScene(1);
        player.transform.position = new Vector3(23, -1, 0);
        deathMenu.SetActive(false);
    }
}
