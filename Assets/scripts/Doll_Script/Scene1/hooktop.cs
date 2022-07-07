using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hooktop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hooktop1;
    public    Rigidbody2D rb;
    public    Rigidbody2D rb2;
    public Rigidbody2D rb3;
     public Rigidbody2D rb5;
     public Rigidbody2D rb7;
    public Rigidbody2D rb8;
    public Rigidbody2D rb9;
    public Rigidbody2D rb0;
    bool aa = false;
    float x;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        aa = true;
        
        rb2.gravityScale = 0;
        rb3.gravityScale = 0;
         rb5.gravityScale = 0;
         rb7.gravityScale = 0;
        rb8.gravityScale = 0;
        rb9.gravityScale = 0;
        rb0.gravityScale = 0;
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = hooktop1.transform.position.x;
        if (aa)
        {
            rb.velocity = Vector2.zero;
            hooktop1.transform.position = new Vector2(x, 3.65f);
            Debug.Log(aa);
            rb.AddForce(new Vector2(-100, 0));
            aa = false;
        }
    }
}
