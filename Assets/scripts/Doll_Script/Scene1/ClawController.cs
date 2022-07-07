using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    private const float time = 0.03f;
    private float timmer = 0;
    private bool istime=true;
    public GameObject zhuazi;
    private float speed=10;
    public Rigidbody2D rb;
    public static bool ise=true;
    private Vector2 a;
    private float x;
    public GameObject lefthook;
    public GameObject righthook;
    float z;
    bool isopen=false;
    bool aa = false;
    bool bb = false;
    bool forceup = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

    }
    
    // Update is called once per frame
    void Update()
    {
        if (ise)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                istime = true;
              //  rb.AddForce(new Vector2(0, -300));
                 ise = false;
                isopen=true;
                Invoke("force", 4);
            }
        }
        if (isopen)
        {
            openhook();
            
        }
        if (aa)
        {   
            rb.gravityScale=1;
            aa = false;
             Invoke("bbb", 5);
        }
        if (bb) { closehook(); }
        if (forceup)
        {
            
            upforce();
            forceup = false;
        }
             float horizontal = Input.GetAxis("Horizontal");
        if(ise)transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
        a = this.transform.position;
        x = a.x;
       
    }
    void bbb()
    {
        bb = true;
    }
    void upforce()
    {
        Debug.Log("force!");
        rb.AddForce(new Vector2(0, 500));
        ise = true;
    }
    void force()
    {
        aa = true;
        istime = true;
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

            if (lefthook.transform.rotation.z > 0) {
                bb = false;
                istime = false;
                forceup = true;           
            }

         }
    }
    void openhook()
    {
        if (istime) timmer += Time.deltaTime;


        if (timmer >= time)
        {
             timmer = 0;
            lefthook.transform.Rotate(new Vector3(0, 0, -0.5f));
            righthook.transform.Rotate(new Vector3(0, 0, 0.5f));

            if (lefthook.transform.rotation.z < -0.2) { istime = false;
                isopen = false;
            }


        }
    }
}
