using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<playermove2>().getspeed() > 5.0f)
            this.GetComponent<TrailRenderer>().enabled = true;
        else
            this.GetComponent<TrailRenderer>().enabled = false;
    }
}
