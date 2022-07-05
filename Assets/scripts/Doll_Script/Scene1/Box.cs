using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Box : MonoBehaviour
{
    public GameObject box;
    public GameObject claw;
    public GameObject doll;
    public GameObject bag;
    bool isopen=false;
    public Rigidbody2D rb;
    private Vector2 ak;
    public static float s;
    public static bool isget=false;
    private bool isup = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isup = true;
    }
  //  private void OnTriggerExit2D(Collider2D collision)
   // {
  //      isup = false;
   // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isopen = !isopen;
            bag.SetActive(isopen);
        }
        if (isup)
        {
            ak = doll.transform.position;
            s = ak.x;
            doll.transform.parent = box.transform;
            isget = true;
            doll.transform.position =new Vector2(s, -3.34f);
            rb.velocity = Vector2.zero;
            Debug.Log("iiiii");
            rb.AddForce(new Vector2(0, 50));
            Doll.crabed = false;
            isup = false;
        }
    }
}
