using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private int score = 0;
    private bool isGameRunning = false;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private float timeSpawnBlocks = 2;
    [SerializeField]
    private float starHighMax;
    [SerializeField]
    private float starHighMin;

    private void Awake()
    {
       PlayerController.EventGameOver += SendANastyWordToTheLog;
    }
    private void Start()
    {
        StartCoroutine(SpawnCoroutin());
    }

    private void SendANastyWordToTheLog()
    {
        Debug.Log("Nasty word");
    }

    IEnumerator SpawnCoroutin()
    {
        while (true)
        {
            Vector3 positionNewBlock = new Vector3(10, Random.Range(starHighMin, starHighMax), 0);
            GameObject newBlock = Instantiate(block);
            newBlock.transform.position = positionNewBlock;
            yield return new WaitForSeconds(timeSpawnBlocks);
        }
    }
}
