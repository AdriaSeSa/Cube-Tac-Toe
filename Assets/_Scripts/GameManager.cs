using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
}
