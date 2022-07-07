using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall3 : MonoBehaviour
{
    public GameObject claw;
    public GameObject wall;
    public Rigidbody2D rb;
    private float y;
    private Vector2 a;
    private bool isup;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isup = true;
        ClawController.ise = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isup = false;
    }

    // Update is called once per frame
    void Update()
    {
        a = claw.transform.position;
        y = a.y;
        if (isup)
        {
            rb.velocity = Vector2.zero;
            claw.transform.position = new Vector2(4.74f, y);
            ClawController.ise = true;
        }
    }
}
