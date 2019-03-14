using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightAndMoveScript : MonoBehaviour {

    #region Variables
    Camera cam;
    Vector3 targetPosition;
    Vector3 lookAtTarget;
    [Header("SELECTED LIGHT AND MOVE SPEED")]
    public GameObject selectionLight;
    public float speed = 1;
    public RaycastHit hit;

    [Header("FIGURE BOOLS")]
    public bool moving;
    public bool selected;
    public bool gmMP;
    public bool gmRPP1;
    public bool gmRPP2;
    public bool gmP1T;
    public bool gmP2T;
    #endregion

    public void Start()
    {
        selectionLight.SetActive(false);
        cam = Camera.main;       
    }

    private void LateUpdate()
    {
        gmP1T = FindObjectOfType<GameManager>().p1t;
        gmP2T = FindObjectOfType<GameManager>().p2t;
        gmMP = FindObjectOfType<GameManager>().movingPhase;
        gmRPP1 = FindObjectOfType<GameManager>().removePhaseP1;
        gmRPP2 = FindObjectOfType<GameManager>().removePhaseP2;

        if (selected == true){
            if (Input.GetButtonDown("Fire2"))
            {
                SetTargetPosition();
                Move();
            }
            if (moving){
                Move();
            }
        }
    }




    #region Highlight And Select A Figure
    public void OnMouseOver()
    {
        if(gmP1T == true && GetComponent<Collider>().tag == "Player1Figure" && gmMP == true && gmRPP1 == false)
        { 
            selectionLight.SetActive(true);
            if (Input.GetButtonDown("Fire1") && selected == false) {
                    selected = true;
                }
                else if (Input.GetButtonDown("Fire1") && selectionLight.activeInHierarchy == true)
                {
                    selected = false;
                }
        }

        else if(gmP2T == true && GetComponent<Collider>().tag == "Player2Figure" && gmMP == true && gmRPP2 == false)
        {
            selectionLight.SetActive(true);
            if (Input.GetButtonDown("Fire1") && selected == false)
            {
                selected = true;
            }
            else if (Input.GetButtonDown("Fire1") && selectionLight.activeInHierarchy == true)
            {
                selected = false;
            }
        }
    }

    private void OnMouseExit()
    {
        if(selected == false)
        {
            selectionLight.SetActive(false);
        }

    }
    #endregion

    #region Get target position
    void SetTargetPosition()
    {
        //Raycast at mouse position and save the position as target position for the figure
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;
            moving = true;
        }
    }
    #endregion

    #region Move function
    //Transform the position of the selected figure to the position invoked by a mouse click, check if figure ended moving to the new position.
    public void Move()
    {
        //Check if the selected position to move is valid (if its a valid Board Position for figures
        if (hit.collider.tag == "BoardPosition")
        {
            //Transform the selected figures position to the desired position ---> MouseInput Position
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

            //Check if the moving of the figure has ended, if it ended, turn off the selection light and remove selection
            if (transform.position == targetPosition)
            {
                selectionLight.SetActive(false);
                moving = false;
                selected = false;
                if(gmMP == true && gmRPP1 == false && gmRPP2==false) { 
                //After a player moves a figure, next player has the turn
                     if(gmP1T == true && gmP2T == false) {
                         FindObjectOfType<GameManager>().p1t = false;
                         FindObjectOfType<GameManager>().p2t = true;
                     }
                     else if (gmP2T == true && gmP1T == false)
                     {
                        FindObjectOfType<GameManager>().p1t = true;
                        FindObjectOfType<GameManager>().p2t = false;
                     }
                }
            }
        }
        else Debug.Log("Invalid position!");
    }
    #endregion
}
