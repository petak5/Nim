using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject victoryPanel;
    public GameObject defeatPanel;
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
        restartGame();
    }

    public void playerRemoveStick(int row)
    {
        if (playerHasToPlay)
        {
            playerHasToPlay = false;
            enableAvailablePlayRowButtons();
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

            // You loose, computer wins
            if (sticks.All(x => x == 0))
            {
                defeatPanel.SetActive(true);
                disableAllPlayButtons();
            }
        }
        print("Game state: " + sticks[0] + " - " + sticks[1] + " - " + sticks[2] + " - " + sticks[3]);
    }

    public void doComputerMove()
    {
        int row, count;
        (row, count) = MiniMaxSolver.getNextMove(sticks);

        if (sticks[row] < count)
        {
            print("FATAL ERROR: solver returned count higher than sticks available!");
        }

        for (int i = 0; i < count; i++)
        {
            sticks[row]--;
            stickObjects[row][sticks[row]].SetActive(false);
        }

        playerHasToPlay = true;
        enableAvailablePlayRowButtons();
        computerMoveButton.SetActive(false);

        // Computer looses, you win
        if (sticks.All(x => x == 0))
        {
            victoryPanel.SetActive(true);
            disableAllPlayButtons();
        }
    }

    private void enableAvailablePlayRowButtons()
    {
        for (int i = 0; i < rowButtons.Length; i++)
        {
            if (sticks[i] > 0)
            {
                rowButtons[i].SetActive(true);
            }
        }
    }

    private void enableAllPlayButtons()
    {
        foreach (GameObject btn in rowButtons)
        {
            btn.SetActive(true);
        }
        computerMoveButton.SetActive(true);
    }

    private void disableAllPlayButtons()
    {
        foreach (GameObject btn in rowButtons)
        {
            btn.SetActive(false);
        }
        computerMoveButton.SetActive(false);
    }

    public void restartGame()
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
        enableAllPlayButtons();
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }
}
