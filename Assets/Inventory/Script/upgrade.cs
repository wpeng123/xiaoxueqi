using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgrade : MonoBehaviour
{
    public GameObject Assemblygroove;
    public GameObject upgrade1;
    int a = 1; 
    int b = 1;
    int c = 1;
    int d = 1;
    int e = 1;
    int f = 1;
    int g = 1;
    bool i = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i)
        {
           // Debug.Log(a + b + c + d + e);
            if (ItemOnDrag.a == 1)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    if (a == 1)
                    {
                        progress.level = progress.level+2;
                        a++;
                        Debug.Log(2); i = false;

                    }
                }
            }
            if (ItemOnDrag.a == 2)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    if (b == 1)
                    {
                        b++;
                        Debug.Log(1);
                        progress.level= progress.level+1; i = false;

                    }
                }
            }
            if (ItemOnDrag.a == 3)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    if (c == 1)
                    {
                        Debug.Log(3);
                        progress2.level2 = progress2.level2 +1;
                        c++;
                        i = false;

                    }
                }
            }
            if (ItemOnDrag.a == 4)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    if (d == 1)
                    {
                        Debug.Log(4);
                        progress3.level3= progress3.level3 +1;
                        d++;
                        i = false;

                    }
                }
            }
            if (ItemOnDrag.a == 5)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    if (e == 1)
                    {
                        Debug.Log(5);
                        progress4.level4 = progress4.level4+1;
                        e++;
                        i = false;

                    }
                }
            }
            if (ItemOnDrag.a == 6)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    Debug.Log("66666666");
                }
            }
            if (ItemOnDrag.a == 7)
            {
                if (Assemblygroove.transform.childCount == 1)
                {
                    Debug.Log("7");
                }
            }
        }
    }
}
