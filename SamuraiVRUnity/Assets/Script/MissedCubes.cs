using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedCubes : MonoBehaviour
{
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ice")
        {
            gm.RemoveScore();
        }
    }
}
