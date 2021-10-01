using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentPlayerTurn;
    private bool isSecondTurnOver;
    
    private void Start()
    {
        currentPlayerTurn = 0;
    }

    public void NextTurn()
    {
        if (!isSecondTurnOver && currentPlayerTurn == 1)
        {
            isSecondTurnOver = true;
            return;
        }
        currentPlayerTurn = currentPlayerTurn == 0 ? 1 : 0;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(0);
    }
    
}
