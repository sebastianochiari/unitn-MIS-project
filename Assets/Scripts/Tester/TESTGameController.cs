using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TESTGameController : MonoBehaviour
{
    public static int NumberOfButtons;
    public static int SequenceLength;

    public static int[] Sequence;

    public delegate void ClickAction();
    public static event ClickAction StartGame;
    
    
    void Start()
    {
        NumberOfButtons = 4;
        SequenceLength = 4;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click"))
        {
            if (StartGame != null)
            {
                CreateSequence();
                StartGame();
            }
        }
    }

    void CreateSequence()
    {
        Sequence = new int[SequenceLength];
        Random random = new Random();
        
        for (int i = 0; i < SequenceLength; i++)
        {
            Sequence[i] = random.Next(NumberOfButtons);
            Debug.Log("Sequence[" + i + "]: " + Sequence[i]);
        }
    }
}
