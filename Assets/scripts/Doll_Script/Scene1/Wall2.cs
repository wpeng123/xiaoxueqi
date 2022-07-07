using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall2 : MonoBehaviour
{
    public GameObject claw;
    public GameObject wall;
    public Rigidbody2D rb;
    private float x;
    private Vector2 a;
    private bool isup;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isup = true;
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
            rb.AddForce(new Vector2(0, 100));
        }
    }
}
