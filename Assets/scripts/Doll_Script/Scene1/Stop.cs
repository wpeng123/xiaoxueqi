using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    private const float time = 0.03f;
    private float timmer = 0;
    public GameObject wall;
    public GameObject lefthook;
    public GameObject righthook;
    public Rigidbody2D rb;
    public Rigidbody2D rb2;
     public Rigidbody2D rb4;
     public Rigidbody2D rb6;
    public Rigidbody2D rb7;
    public Rigidbody2D rb8;
    public Rigidbody2D rb9;
    public Rigidbody2D rb0;
    bool istime = false;
    bool isopen = false;
    bool bb = false;
    bool aa = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        istime = true;
        aa = true;
        rb.velocity = Vector2.zero;
        Invoke("down",2);
        Invoke("bbb", 4); 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void closehook()
    {
        rb.gravityScale = 0;
        if (istime) timmer += Time.deltaTime;

        if (timmer >= time)
        {
            timmer = 0;
            lefthook.transform.Rotate(new Vector3(0, 0, 0.5f));
            righthook.transform.Rotate(new Vector3(0, 0, -0.5f));

            if (lefthook.transform.rotation.z > 0)
            {
                bb = false;
                istime = false;
                 
            }

        }
    }
    void bbb()
    {
        bb=true;
    }
    void openhook()
    {
        if (istime) timmer += Time.deltaTime;


        if (timmer >= time)
        {
            timmer = 0;
            lefthook.transform.Rotate(new Vector3(0, 0, -0.5f));
            righthook.transform.Rotate(new Vector3(0, 0, 0.5f));

            if (lefthook.transform.rotation.z < -0.2)
            {
                istime = false;
                isopen = false;
                aa = false;
            }


        }
    }
    private void down()
    {
        rb2.gravityScale = 0.3f;
         rb4.gravityScale = 0.3f;
         rb6.gravityScale = 0.3f;
        rb7.gravityScale = 0.3f;
        rb8.gravityScale = 0.3f;
        rb9.gravityScale = 0.3f;
        rb0.gravityScale = 0.3f;
        istime = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (aa)
        {
            openhook();
            
        }
        if (bb)
        {
            closehook();
        }
    }
}
