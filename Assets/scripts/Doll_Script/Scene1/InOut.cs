using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InOut : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public Rigidbody2D rb2;
    private bool aa=true;
    private bool aaa = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Invoke("force", 5);
    }
    void force()
    {
        if (aa) 
        { 
            rb.AddForce(new Vector2(0, 500));
            rb2.AddForce(new Vector2(0, 500));
        }
        Invoke("delete", 2);
        Debug.Log("aaaaaa");
        aa = false;
    }
    void delete()
    {
        if (aaa)
        {
            rb.velocity = Vector2.zero;
            rb2.velocity = Vector2.zero;
            aaa = false;
        }
    }
}
