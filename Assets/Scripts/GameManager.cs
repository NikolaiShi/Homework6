using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private int score = 0;
    private bool isGameRunning = false;
    [SerializeField]
    private PlayerController player;

    private void Awake()
    {
       PlayerController.EventGameOver += SendANastyWordToTheLog;
    }

    private void SendANastyWordToTheLog()
    {
        Debug.Log("Nasty word");
    }
}
