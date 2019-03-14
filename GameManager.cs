using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //GameManager script controls the phases of the game, disables and enables players turns, displays the mentioned and tracks the number of figures on board

    #region Variables
    public AudioSource endPlacingPhaseSound;
    [Header("TEXT VARIABLES")]
    public Text text;
    public Text text2;
    public Text p1FiguresOnBoard;
    public Text p2FiguresOnBoard;
    public Text selectedFigurePosition;
    [Header("GAME MANAGER BOOLS")]
    public bool p1t;
    public bool p2t;
    public bool placingPhase;
    public bool movingPhase;
    public bool removePhaseP1;
    public bool removePhaseP2;
    public int counterP1;
    public int counterP2;


    #endregion

    #region Update Function
    private void Update()
    {
        CheckIfRemovePhase();
        CheckCounters();
        CheckPlayerTurn();
        ShowPhase();
        Restart();

        if(movingPhase == true)
        {
            removePhaseP1 = false;
            removePhaseP2 = false;
        }
    }
    #endregion
    private void LateUpdate()
    {
        if (removePhaseP1 == true || removePhaseP2 == true)
        {
            movingPhase = false;
        }
    }
    #region Check Figure Counters
    public void CheckCounters()
    {
        if(counterP1 == 9 && counterP2 == 9)
        {
            counterP1 = 10;
            movingPhase = true;
            placingPhase = false;
        }
    }
    #endregion



    #region Check For 3 Same Figures On A Line
    void CheckIfRemovePhase()
    {
        if(p1t==true && p2t == false && removePhaseP1 == true && movingPhase == false)
        { 
            if (Input.GetButtonDown("Fire1"))
            {
                     FindObjectOfType<RemoveOtherPlayerFigure>().SelectFigureToDestroy();

            }
        }

        else if (p2t == true && p1t == false && removePhaseP2 == true && movingPhase == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                    FindObjectOfType<RemoveOtherPlayerFigure>().SelectFigureToDestroy();
            }
        }
    }

    #endregion

    #region Show Phase 
    void ShowPhase()
    {

        if (movingPhase == false && placingPhase == false)
        {
            text2.text = "Remove Phase!";
        }

        else if (placingPhase == true)
        {
            text2.text = "Placing Phase!";
        }

        else if (movingPhase == true && removePhaseP1 == false && removePhaseP2 == false)
        {
            text2.text = "Moving Phase!";
        }
    }
    #endregion

    #region Show Players Turn
    void CheckPlayerTurn()
    {
        if (p1t == true)
        {
            text.text = "Player 1 turn!";
        }
        if (p2t == true)
        {
            text.text = "Player 2 turn!";
        }
        p1FiguresOnBoard.text = "Player 1 figures: " + counterP1;
        p2FiguresOnBoard.text = "Player 2 figures: " + counterP2;
    }
    #endregion

    #region Restart
    void Restart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion
}
