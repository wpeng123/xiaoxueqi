using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progress3 : MonoBehaviour
{
    public static int level3 = 6;
    public GameObject a1;
    public GameObject a2;
    public GameObject a3;
    public GameObject a4;
    public GameObject a5;
    public GameObject a6;
    public GameObject a7;
    public GameObject a8;
    public GameObject a9;
    public GameObject a10;

    // Update is called once per frame
    void Update()
    {
        if (level3 == 0)
        {
            a1.SetActive(false);
            a2.SetActive(false);
            a3.SetActive(false);
            a4.SetActive(false);
            a5.SetActive(false);
            a6.SetActive(false);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);


        }
        if (level3 == 1)
        {
            a1.SetActive(true);
            a2.SetActive(false);
            a3.SetActive(false);
            a4.SetActive(false);
            a5.SetActive(false);
            a6.SetActive(false);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3== 2)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(false);
            a4.SetActive(false);
            a5.SetActive(false);
            a6.SetActive(false);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3 == 3)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(false);
            a5.SetActive(false);
            a6.SetActive(false);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3 == 4)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(false);
            a6.SetActive(false);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3 == 5)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(true);
            a6.SetActive(false);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3 == 6)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(true);
            a6.SetActive(true);
            a7.SetActive(false);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3 == 7)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(true);
            a6.SetActive(true);
            a7.SetActive(true);
            a8.SetActive(false);
            a9.SetActive(false);
            a10.SetActive(false);
        }
         if (level3 == 8)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(true);
            a6.SetActive(true);
            a7.SetActive(true);
            a8.SetActive(true);
            a9.SetActive(false);
            a10.SetActive(false);
        }
        if (level3 == 9)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(true);
            a6.SetActive(true);
            a7.SetActive(true);
            a8.SetActive(true);
            a9.SetActive(true);
            a10.SetActive(false);
        }
        if (level3 == 10)
        {
            a1.SetActive(true);
            a2.SetActive(true);
            a3.SetActive(true);
            a4.SetActive(true);
            a5.SetActive(true);
            a6.SetActive(true);
            a7.SetActive(true);
            a8.SetActive(true);
            a9.SetActive(true);
            a10.SetActive(true);
        }
    }
}
