using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabed : MonoBehaviour
{
    // Start is called before the first frame update
   public static bool i = false;
    public GameObject bag;
    bool isopen=false;
    bool s = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        s = true;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isopen = !isopen;
            bag.SetActive(isopen);
        }
        if (s)
        {
            i = true;
            s = false;
        }
    }
}
