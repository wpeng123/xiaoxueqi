using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall1 : MonoBehaviour
{
    public GameObject claw;
    public GameObject wall;
    public Rigidbody2D rb;
    private float x;
    private Vector2 a;
    private bool isup;
    public static bool ppp=false;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isup = true;
        ClawController.ise = true;
        if(Doll.iscrab)Doll.is33 = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isup = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        a = claw.transform.position;
        x = a.x;
        if (isup)
        {
            rb.velocity = Vector2.zero;
            claw.transform.position=new Vector2(x, 4.58f);
            if (Doll.crabed == true)
            {
                claw.transform.position = new Vector2(x, 4.58f);
                ClawController.ise = false;
                rb.AddForce(new Vector2(-200, 0));
                Debug.Log("121111");
                Doll.crabed = false;
                ppp = true;
            }
            isup = false;
        }
    }
}
