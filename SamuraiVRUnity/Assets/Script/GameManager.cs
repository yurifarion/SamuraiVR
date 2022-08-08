using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private KatanaSlicer katanaAgent;
    private FruitSpawner fruitSpawnerAgent;

    //UI
    public TMPro.TextMeshPro introductionText;
    public TMPro.TextMeshPro countDownText;
    public TMPro.TextMeshPro scoreText;


    //CountDown variables
    private float timerCountDown;


    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        katanaAgent = GameObject.FindGameObjectWithTag("Katana").GetComponent<KatanaSlicer>();
        fruitSpawnerAgent = GetComponent<FruitSpawner>();

        scoreText.text = "Score:" + score;
    }

    // Update is called once per frame
    void Update()
    {
        //If score are negative is gameOver
        if(score < 0)
        {
            SceneManager.LoadScene("GameOverExplosion");
        }

        //Player grab katana let the coutDown Starts
        if (katanaAgent.isGrabbed && !fruitSpawnerAgent.IsGameStarted())
        {
            introductionText.gameObject.SetActive(false);
            countDownText.gameObject.SetActive(true);

            timerCountDown += Time.deltaTime;

            countDownText.text = "" + ((int)timerCountDown);

            if (timerCountDown >= 3)
            {
                fruitSpawnerAgent.startSpawn();
                countDownText.gameObject.SetActive(false);

            }
           
        }
        else if(!fruitSpawnerAgent.IsGameStarted())
        {
            timerCountDown = 0;
            fruitSpawnerAgent.stopSpawn();
        }
    }

    //By default all scores added are equal to 1
    public void AddScore()
    {
        score += 1;
        //Update Text
        scoreText.text = "Score:" + score;
    }
    //By default all scores decreased are equal to 10
    public void RemoveScore()
    {
        score -= 10;
        //Update Text
        scoreText.text = "Score:" + score;
    }
}
