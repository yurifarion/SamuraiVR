using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    // this class wil spawn fruits randomly within a constant time interval
    public GameObject fruitPrefab;
    public GameObject BombPrefab;
    public float timeInterval = 1;
    private float timer = 0;

    //Spawner Points
    public Transform spawnerPoint1;
    public Transform spawnerPoint2;
    public Transform spawnerPoint3;
    public Transform spawnerPoint4;

    public float pushStrength = 15;
    private bool readyToStart = false;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = timeInterval;
    }
    public void startSpawn()
    {
        readyToStart = true;
    }
    public void stopSpawn()
    {
        readyToStart = false;
    }
    public bool IsGameStarted()
    {
        return readyToStart;
    }
    // Update is called once per frame
    void Update()
    {
        if (readyToStart && timer <= 0)
        {

            //Decide if it will spawn Bomb or Ice cube
            int bombOrIceRand = Random.Range(0, 10);// 10% of chance to spawn Bomb

            //Spawn Ice
            if(bombOrIceRand < 8)
            { 
                //pick randomly one spawnerPoint
                int rand = Random.Range(1, 5);
                Transform spawnTransform = spawnerPoint1;
                if (rand == 2) spawnTransform = spawnerPoint2;
                if (rand == 3) spawnTransform = spawnerPoint3;
                if (rand == 4) spawnTransform = spawnerPoint4;

                GameObject spawn = Instantiate(fruitPrefab, spawnTransform.position, spawnTransform.rotation);

                //randomize the size
                float randomSize = Random.Range(0.170f, 0.500f);
                spawn.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                //this prefab should have a rigidBody
                spawn.GetComponent<Rigidbody>().AddForce(new Vector3(pushStrength, 0, 0));  //make it go forward to player

                Vector3 randonRotation = new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5));
                spawn.GetComponent<Rigidbody>().AddTorque(randonRotation * 100);  //make it rotate
                timer = timeInterval;
            }
            else
            {
                //pick randomly one spawnerPoint
                int rand = Random.Range(1, 5);
                Transform spawnTransform = spawnerPoint1;
                if (rand == 2) spawnTransform = spawnerPoint2;
                if (rand == 3) spawnTransform = spawnerPoint3;
                if (rand == 4) spawnTransform = spawnerPoint4;

                GameObject spawn = Instantiate(BombPrefab, spawnTransform.position, spawnTransform.rotation);

               
                //this prefab should have a rigidBody
                spawn.GetComponent<Rigidbody>().AddForce(new Vector3(pushStrength, 0, 0));  //make it go forward to player

                Vector3 randonRotation = new Vector3(Random.Range(0, 5), Random.Range(0, 2), Random.Range(0, 2));
                spawn.GetComponent<Rigidbody>().AddTorque(randonRotation * 100);  //make it rotate
                timer = timeInterval;
            }
        }
        else timer -= Time.deltaTime;
    }
}
