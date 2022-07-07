using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    private const float time = 0.03f;
    private float timmer = 0;
    byte x = 255;
    public Material material;
    public GameObject aa;
    bool xx=true;
    byte a = 1;
    // Start is called before the first frame update
    void Start()
    {
        //material.color = new Color32(0, 0, 0, 255);
        aa.GetComponent<SpriteRenderer>().material.color = new Color32(0,0,0,255);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("aaa", 1);
    }
   
    void aaa()
    {
        if (xx) timmer += Time.deltaTime;


        if (timmer >= time)
        {
            timmer = 0;
            aa.GetComponent<SpriteRenderer>().material.color = new Color32(0, 0, 0, x--);
            if (x == 0) xx = false;


        }
    }
}
