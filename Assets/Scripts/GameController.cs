﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Para retornar ao menu
using TMPro; //Para usar o text mesh pro
using UnityEngine.EventSystems; //para controlar o evento selecionado caso o pause seja pelo joystick

public class GameController : MonoBehaviour
{
    //No caso do GameController prefiro receber quase tudo em public. Fica mais fácil de modificar componentes depois
    public GameObject pauseCanvas;
    public GameObject endGameCanvas;

    public GameObject pauseButton;
    public GameObject rtButton;
    public GameObject restartButton;

    public GameObject gameOverText;

    private TextMeshProUGUI texto;
    private bool gameHasEnded;
    private PlayerMovement player;

    public string[] youLostTextList;
    private string youWin = "You Win!";

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 

        // Atenção TextMeshPro != TextMeshProUGUI
        texto = gameOverText.GetComponent<TextMeshProUGUI>();

        if(pauseCanvas.activeSelf || endGameCanvas.activeSelf)
        {
            pauseCanvas.SetActive(false);
            endGameCanvas.SetActive(false);
        }
        gameHasEnded = false;
    }

    public void GameEnd() //Se o player morrer
    {
        if (!gameHasEnded)//Se o jogo não acabou, acabar.
        {
            gameHasEnded = true;

            player.enabled = false;//Parar com o loop de update do player
            //Não posso fazer checkpoints fucionais pois https://stackoverflow.com/questions/55815916/unity-new-input-system-cant-enable-controls-after-disabling-them

            texto.SetText(youLostTextList[Random.Range(0, youLostTextList.Length)]); //Escolher um texto dentro do vetor de strings

            endGameCanvas.SetActive(true);
            pauseButton.SetActive(false);

            EventSystem.current.SetSelectedGameObject(restartButton);//Para caso esteja a usar o controle, eu tenha a certeza que consegue usar o menu.
                                                                     //Tirei daqui https://answers.unity.com/questions/883220/how-to-change-selected-button-in-eventsystem-or-de.html
        }

    }

    public void Win()
    {
        if (!gameHasEnded)//Se o jogo não acabou, acabar.
        {
            gameHasEnded = true;

            player.enabled = false;//Parar com o loop de update do player
            //Não posso fazer checkpoints fucionais pois https://stackoverflow.com/questions/55815916/unity-new-input-system-cant-enable-controls-after-disabling-them

            texto.SetText(youWin); //Escolher um texto dentro do vetor de strings

            endGameCanvas.SetActive(true);
            pauseButton.SetActive(false);

            EventSystem.current.SetSelectedGameObject(restartButton);//Para caso esteja a usar o controle, eu tenha a certeza que consegue usar o menu.
                                                                     //Tirei daqui https://answers.unity.com/questions/883220/how-to-change-selected-button-in-eventsystem-or-de.html
        }
    }


    public void GamePause() 
    {
        if (Time.timeScale != 0)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(rtButton); //Para caso esteja a usar o controle, eu tenha a certeza que consegue usar o menu.
                                                                 //Tirei daqui https://answers.unity.com/questions/883220/how-to-change-selected-button-in-eventsystem-or-de.html
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void GameTutorial(bool active)
    {
        if (active)
        {
            Time.timeScale = 0;
            pauseButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            pauseButton.SetActive(true);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
