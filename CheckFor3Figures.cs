using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFor3Figures : MonoBehaviour {

    #region Variables
    public int numberP1;
    public int numberP2;
    [Header("GAME MANAGER BOOLS")]
    public bool player1Turn;
    public bool player2Turn;
    public bool removePhaseP1;
    public bool removePhaseP2;
    public bool movingPhase;
    #endregion

    #region Update
    private void LateUpdate()
    {
        GetGameManagerBooleans();
        CheckPhases();
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(gameObject.name + "Blue "+ numberP1);
            Debug.Log(gameObject.name + "Red "+ numberP2);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
  

        }
    }
    #endregion

    #region On Trigger Enter
    //On trigger enter add the players figure to the list
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player1Figure") )
        {
            numberP1++;
        }

        if ((other.gameObject.tag == "Player2Figure"))
        {
            numberP2++;
        }
    }
    #endregion

    #region On Trigger Exit
    private void OnTriggerExit(Collider other)
    {

        if ((other.gameObject.tag == "Player1Figure"))
        {
            numberP1--;
        }

        if ((other.gameObject.tag == "Player2Figure"))
        {
            numberP2--;
        }
    }
    #endregion

    #region Check And Change Phases
    //If a player performs a mill change the phase to remove phase for the same player and keep track of figures on each line
    void CheckPhases()
    {
        if(FindObjectOfType<GameManager>().placingPhase == false)
        {
            ChangePhaseBasedOnFigures();
        }
    }

   public void ChangePhaseBasedOnFigures()
    {
        if(numberP1 == 3 && movingPhase == true && removePhaseP2 == false && player1Turn == true)
        {
            FindObjectOfType<GameManager>().removePhaseP1 = true;
            FindObjectOfType<GameManager>().movingPhase = false;
        }

        if (numberP2 == 3 && movingPhase == true && removePhaseP1 == false && player2Turn == true)
        {
            FindObjectOfType<GameManager>().removePhaseP2 = true;
            FindObjectOfType<GameManager>().movingPhase = false;
        }

    }
    #endregion

    void GetGameManagerBooleans()
    {
        player1Turn = FindObjectOfType<GameManager>().p1t;
        player2Turn = FindObjectOfType<GameManager>().p2t;
        movingPhase = FindObjectOfType<GameManager>().movingPhase;
        removePhaseP1 = FindObjectOfType<GameManager>().removePhaseP1;
        removePhaseP2 = FindObjectOfType<GameManager>().removePhaseP2;
    }
}

