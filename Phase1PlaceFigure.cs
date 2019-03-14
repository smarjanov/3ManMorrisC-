using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1PlaceFigure : MonoBehaviour {
    public Transform figureP1;
    public Transform figureP2;
    public AudioSource placeSound;

    public GameObject gm;

    private void Start()
    {
        gm.GetComponent<GameManager>().p1t = true;
        gm.GetComponent<GameManager>().p2t = false;
        gm.GetComponent<GameManager>().placingPhase = true;
        gm.GetComponent<GameManager>().movingPhase = false;
    }

    public void OnMouseDown()
    {
        if (gm.GetComponent<GameManager>().placingPhase == true)
        {
            if (gm.GetComponent<GameManager>().p1t == true && gm.GetComponent<GameManager>().p2t == false)
            {
                gm.GetComponent<GameManager>().p2t = true;
                gm.GetComponent<GameManager>().p1t = false;
                Player1SetFigure();

            }
            else if (gm.GetComponent<GameManager>().p2t == true && gm.GetComponent<GameManager>().p1t == false)
            {
                gm.GetComponent<GameManager>().p1t = true;
                gm.GetComponent<GameManager>().p2t = false;
                Player2SetFigure();

            }
        }else if(gm.GetComponent<GameManager>().movingPhase == true)
        {
            gm.GetComponent<GameManager>().p1t = FindObjectOfType<HighlightAndMoveScript>().gmP1T;
            gm.GetComponent<GameManager>().p2t = FindObjectOfType<HighlightAndMoveScript>().gmP2T;
        }
    }



    void Player1SetFigure()
    {
        Instantiate(figureP1, transform.position, transform.rotation);
        gm.GetComponent<GameManager>().counterP1++;
        placeSound.Play();
    }

    void Player2SetFigure()
    {
        Instantiate(figureP2, transform.position, transform.rotation);
        gm.GetComponent<GameManager>().counterP2++;
        placeSound.Play();
    }
}


