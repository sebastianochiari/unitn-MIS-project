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
    public GameObject gameSuccessUI;
    public GameObject gameOverUI;

    private void Awake()
    {
        BoardController.ShowTutorialUI += OnShowTutorialUI;
        BoardController.ShowSequenceUI += OnShowSequenceUI;
        GameController.ShowGameStartUI += OnShowGameStartUI;
        GameController.ShowGameSuccessUIPanel += OnShowGameSuccessUIPanel;
        GameController.ShowGameOverUIPanel += OnShowGameOverUIPanel;
    }

    private void OnDestroy()
    {
        BoardController.ShowTutorialUI -= OnShowTutorialUI;
        BoardController.ShowSequenceUI -= OnShowSequenceUI;
        GameController.ShowGameStartUI -= OnShowGameStartUI;
        GameController.ShowGameSuccessUIPanel -= OnShowGameSuccessUIPanel;
        GameController.ShowGameOverUIPanel -= OnShowGameOverUIPanel;
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

    private void OnShowGameSuccessUIPanel()
    {
        StartCoroutine(ShowEndGameUIPanelCoroutine(gameSuccessUI));
    }

    private void OnShowGameOverUIPanel()
    {
        StartCoroutine(ShowEndGameUIPanelCoroutine(gameOverUI));
    }

    IEnumerator ShowEndGameUIPanelCoroutine(GameObject gameObject)
    {
        yield return new WaitForSeconds(6f);

        gameObject.SetActive(true);
        
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ReturnHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
