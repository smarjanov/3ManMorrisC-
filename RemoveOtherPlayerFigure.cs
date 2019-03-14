using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOtherPlayerFigure : MonoBehaviour {

    #region Variables
    public Camera cam;
    public bool player1Turn;
    public bool player2Turn;
    public bool removePhaseP1;
    public bool removePhaseP2;
    public bool movingPhase;
    public RaycastHit hit;
    #endregion

    private void Start()
    {
        cam = Camera.main;
    }


    private void LateUpdate()
    {
        GetGameManagerBooleans();
        if (removePhaseP1 == true || removePhaseP2 == true)
        {
            movingPhase = false;
        }
    }

 

    public void SelectFigureToDestroy()
    {
        RemoveFigure();   
    }

    void RemoveFigure()
    {
        if(removePhaseP1 == true && movingPhase == false && player1Turn == true && Input.GetButton("Fire1"))
        { 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
              if (Physics.Raycast(ray, out hit, 1000) && hit.collider.gameObject.tag == "Player2Figure")
              {
                  hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                  hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(0, 55, 0);
                  Destroy(hit.collider.gameObject, 5);

                  FindObjectOfType<CheckFor3Figures>().ChangePhaseBasedOnFigures();
                  FindObjectOfType<GameManager>().removePhaseP1 = false;
                  FindObjectOfType<GameManager>().p1t = false;
                  FindObjectOfType<GameManager>().movingPhase = true;
                  FindObjectOfType<GameManager>().p2t = true;
            }
        }

        else if (removePhaseP2 == true && movingPhase == false && player2Turn == true && Input.GetButton("Fire1"))
        {
             Ray ray = cam.ScreenPointToRay(Input.mousePosition);
             if (Physics.Raycast(ray, out hit, 1000) && hit.collider.gameObject.tag == "Player1Figure")
             {
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(0, 55, 0);
                Destroy(hit.collider.gameObject, 5);

                FindObjectOfType<CheckFor3Figures>().ChangePhaseBasedOnFigures();
                FindObjectOfType<GameManager>().removePhaseP1 = false;
                FindObjectOfType<GameManager>().movingPhase = true;
                FindObjectOfType<GameManager>().p2t = false;
                FindObjectOfType<GameManager>().p1t = true;
            }
        }
    }

    void GetGameManagerBooleans()
    {
        player1Turn = FindObjectOfType<GameManager>().p1t;
        player2Turn = FindObjectOfType<GameManager>().p2t;
        removePhaseP1 = FindObjectOfType<GameManager>().removePhaseP1;
        removePhaseP2 = FindObjectOfType<GameManager>().removePhaseP2;
        movingPhase = FindObjectOfType<GameManager>().movingPhase;
    }


}

