using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll : MonoBehaviour
{
    public GameObject claw;
    public   GameObject doll;
    public Rigidbody2D rb;
    public static  bool iscrab;
    public static bool is33 = true ;
    public static bool crabed=false ;
    public bool ak;
    public bool kk;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        iscrab = true;
    }
    private void OnTriggerExit(Collider other)
    {
        iscrab = false;
    }
    // Update is called once per frame
    void Update()
    {
        ak = crabed;
        kk = iscrab;
        if (iscrab)
        {
            rb.velocity = Vector2.zero;
            
            if (true) { rb.AddForce(new Vector2(0, 200)); }
            this.transform.parent = claw.transform;
            crabed = true;
            iscrab = false;
         }
    }
}
