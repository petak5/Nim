using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] sticksRow1;
    public GameObject[] sticksRow2;
    public GameObject[] sticksRow3;
    public GameObject[] sticksRow4;
    private GameObject[][] stickObjects;
    public GameObject[] rowButtons;
    public GameObject computerMoveButton;
    private int[] sticks;
    private bool playerHasToPlay;
    private int playerPlayedRow;

    private void Start()
    {
        stickObjects = new[]
        {
            sticksRow1,
            sticksRow2,
            sticksRow3,
            sticksRow4
        };
        initGame();
    }

    public void removeStick(int row)
    {
        if (playerHasToPlay)
        {
            playerHasToPlay = false;
            foreach (GameObject btn in rowButtons)
            {
                btn.SetActive(true);
            }
            playerPlayedRow = row;
            computerMoveButton.SetActive(true);
        }

        if (sticks[row] > 0 && row == playerPlayedRow)
        {
            sticks[row]--;
            stickObjects[row][sticks[row]].SetActive(false);

            if (sticks[row] <= 0)
            {
                rowButtons[row].SetActive(false);
            }
        }
    }

    public void doComputerMove()
    {
        playerHasToPlay = true;
        foreach (GameObject btn in rowButtons)
        {
            btn.SetActive(true);
        }
        computerMoveButton.SetActive(false);
    }

    public void restartGame()
    {
        initGame();
    }

    private void initGame()
    {
        playerHasToPlay = true;
        playerPlayedRow = -1;
        sticks = new[] { 1, 3, 5, 7 };
        foreach (GameObject[] row in stickObjects)
        {
            foreach (GameObject obj in row)
            {
                obj.SetActive(true);
            }
        }
        foreach (GameObject btn in rowButtons)
        {
            btn.SetActive(true);
        }
        computerMoveButton.SetActive(true);
    }
}
