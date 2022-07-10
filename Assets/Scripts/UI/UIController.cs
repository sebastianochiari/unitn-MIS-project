using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public static bool GameIsPaused;

    public GameObject gameSetupUI;
    public GameObject pauseMenuUI;
    public GameObject startTutorialUI;
    public GameObject startSequenceUI;
    public GameObject gameStartUI;

    private void Awake()
    {
        BoardController.ShowTutorialUI += OnShowTutorialUI;
        BoardController.ShowSequenceUI += OnShowSequenceUI;
        GameController.ShowGameStartUI += OnShowGameStartUI;
    }

    private void OnDestroy()
    {
        BoardController.ShowTutorialUI -= OnShowTutorialUI;
        BoardController.ShowSequenceUI -= OnShowSequenceUI;
        GameController.ShowGameStartUI -= OnShowGameStartUI;
    }

    void Start()
    {
        GameIsPaused = true;
        
        gameSetupUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !(gameSetupUI.activeInHierarchy))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void OnShowTutorialUI()
    {
        StartCoroutine(OnShowPopupUICoroutine(startTutorialUI));
    }
    
    private void OnShowSequenceUI()
    {
        StartCoroutine(OnShowPopupUICoroutine(startSequenceUI));
    }

    private void OnShowGameStartUI()
    {
        StartCoroutine(OnShowPopupUICoroutine(gameStartUI));
    }

    IEnumerator OnShowPopupUICoroutine(GameObject gameobject)
    {
        yield return new WaitForSeconds(0.25f);
        
        gameobject.SetActive(true);
        
        yield return new WaitForSeconds(8f);
        
        gameobject.SetActive(false);
        
        yield break;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void ReturnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
