using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int bestScore = 0;
    private bool isGameRunning = false;
    private List<GameObject> Barriers;
    private Coroutine respawn;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private int numberOfBarriers = 8;
    [SerializeField]
    private float timeSpawnBlocks = 2;
    [SerializeField]
    private float startHeightMin;
    [SerializeField]
    private float startHeightMax;
    [SerializeField]
    private GameObject startUI;
    [SerializeField]
    private GameObject gamePlayUI;
    [SerializeField]
    private GameObject resultUI;
    [SerializeField]
    private TextMeshProUGUI scoreLable;
    [SerializeField]
    private TextMeshProUGUI resultScore;
    [SerializeField]
    private TextMeshProUGUI bestResult;

    private void Awake()
    {
       PlayerController.EventGameOver += GameOver;
       PlayerController.AddOnePoint += UpdateScore;
       PlayerController.StartGame += StartGame; 
    }
    private void Start()
    {
        Barriers = new List<GameObject>();
    }

    private void StartGame()
    {
        startUI.SetActive(false);
        gamePlayUI.SetActive(true);
        isGameRunning = true;
        score = 0;
        scoreLable.text = score.ToString();

        respawn = StartCoroutine(SpawnCoroutin());
        
        
       
    }
    private void UpdateScore()
    {
        score++;
        scoreLable.text = score.ToString();
    }

    private void GameOver()
    {
        gamePlayUI.SetActive(false);
        resultUI.SetActive(true);
        
        if (score > bestScore) bestScore = score;
        resultScore.text = score.ToString();
        bestResult.text = bestScore.ToString();
        
        isGameRunning = false;
        StopCoroutine(respawn);
        foreach (GameObject pipe in Barriers)
        {
            pipe.GetComponent<ObjectMovement>().speed = 0;
        }
    }

    public void RestartGame()
    {
        resultUI.SetActive(false);
        startUI.SetActive(true);
        
        foreach (GameObject pipe in Barriers)
        {
            Destroy(pipe);
        }
        Barriers.Clear();
    }

    IEnumerator SpawnCoroutin()
    {
        for (int i = 0; i < numberOfBarriers; i++)
        {
            Vector3 positionNewBlock = new Vector3(10, Random.Range(startHeightMin, startHeightMax), 0);
            GameObject newObject = Instantiate(block, positionNewBlock, Quaternion.identity);
            newObject.name = "Block" + i;
            Barriers.Add(newObject);
            
            yield return new WaitForSeconds(timeSpawnBlocks);
            
            if (i == numberOfBarriers - 1)
            {
                respawn = StartCoroutine(RespawnCoroutine());
            }
        }
    }

    IEnumerator RespawnCoroutine()
    {
        while(true)
        {
            foreach (GameObject pipe in Barriers)
            {
                Vector3 positionNewBlock = new Vector3(10, Random.Range(startHeightMin, startHeightMax), 0);
                pipe.transform.position = positionNewBlock;
                
                yield return new WaitForSeconds(timeSpawnBlocks);
            }
        }
    }
}
