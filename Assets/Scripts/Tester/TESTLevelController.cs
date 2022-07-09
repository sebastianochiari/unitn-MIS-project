using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TESTLevelController : MonoBehaviour
{
    public GameObject parent;
    public static List<GameObject> Buttons = new List<GameObject>();

    public Material material;
    public Material material2;

    private void Awake()
    {
        TESTGameController.StartGame += SetupLevel;
    }

    private void Start()
    {
        foreach (Transform child in parent.transform)
        {
            if (child.CompareTag("Button"))
            {
                Buttons.Add(child.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        TESTGameController.StartGame -= SetupLevel;
    }

    void SetupLevel() 
    {
        for (int i = 0; i < TESTGameController.NumberOfButtons; i++)
        {
            Buttons[i].SetActive(true);
        }
        StartTutorial();
    }

    private void StartTutorial()
    {
        StartCoroutine(StartTutorialCoroutine());
        Debug.Log("Ended StartTutorial()");
    }

    IEnumerator StartTutorialCoroutine()
    {
        Debug.Log("Starting StartTutorialCoroutine()");
        
        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < TESTGameController.SequenceLength; i++)
        {
            int buttonNumber = TESTGameController.Sequence[i];

            Renderer buttonRenderer = Buttons[i].GetComponent<Renderer>();

            buttonRenderer.material = material;
            
            yield return new WaitForSeconds(.25f);
            
            buttonRenderer.material = new Material(Shader.Find("Diffuse"));
            
            yield return new WaitForSeconds(2f);
        }

        ShowSequence();

        Debug.Log("Ended StartTutorialCoroutine");
    }

    private void ShowSequence()
    {
        StopAllCoroutines();
        StartCoroutine(ShowSequenceCoroutine());
    }

    IEnumerator ShowSequenceCoroutine()
    {
        Debug.Log("Starting ShowSequenceCoroutine()");
        
        yield return new WaitForSeconds(5f);
        
        for (int i = 0; i < TESTGameController.SequenceLength; i++)
        {
            int buttonNumber = TESTGameController.Sequence[i];

            Renderer buttonRenderer = Buttons[i].GetComponent<Renderer>();

            buttonRenderer.material = material2;
            
            yield return new WaitForSeconds(1f);
            
            buttonRenderer.material = new Material(Shader.Find("Diffuse"));
            
            yield return new WaitForSeconds(2f);
        }
        
        // send event: readyToPlay
    }
}
