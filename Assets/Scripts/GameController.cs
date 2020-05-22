using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Para retornar ao menu
using TMPro; //Para usar o text mesh pro

public class GameController : MonoBehaviour
{
    //No caso do GameController prefiro receber quase tudo em public. Fica mais fácil de modificar componentes depois
    public GameObject pauseCanvas;
    public GameObject endGameCanvas;

    public GameObject pauseButton;
    public GameObject gameOverText;

    private TextMeshProUGUI texto;

    private PlayerMovement player;

    public string[] textlist;

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

    }

    public void GameEnd() //Se o player morrer
    {
        player.enabled = false;
        texto.SetText(textlist[Random.Range(0, textlist.Length)]); //Escolher um texto dentro do vetor de strings
        
        endGameCanvas.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void GamePause() 
    {
        if (Time.timeScale != 0)
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReturnToCheckpoint()
    {

    }
}
