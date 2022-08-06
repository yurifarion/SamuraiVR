using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private KatanaSlicer katanaAgent;
    public FruitSpawner fruitSpawnerAgent;

    public TMPro.TextMeshPro introductionText;
    public TMPro.TextMeshPro countDownText;

    //CountDown variables
    private float timerCountDown;
    

    //UI of Introduction

    // Start is called before the first frame update
    void Start()
    {
        katanaAgent = GameObject.FindGameObjectWithTag("Katana").GetComponent<KatanaSlicer>();
        //fruitSpawnerAgent = GetComponent<FruitSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player grab katana let the coutDown Starts
        if (katanaAgent.isGrabbed)
        {
            introductionText.gameObject.SetActive(false);
            countDownText.gameObject.SetActive(true);

            timerCountDown += Time.deltaTime;

            countDownText.text = "" + ((int)timerCountDown);

            if (timerCountDown >= 3) fruitSpawnerAgent.startSpawn();
        }
        else
        {
            timerCountDown = 0;
            fruitSpawnerAgent.stopSpawn();
        }

        

    }
}
