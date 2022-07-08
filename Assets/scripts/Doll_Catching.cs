using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doll_Catching : MonoBehaviour
{
    public GameObject scene1;
    public GameObject maincamara;
    public GameObject canvas;
    public GameObject upgradefinish;
    public GameObject all;
    public static bool a=false;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        a = gamemanager.doll;
        if(a)
            scene1.SetActive(gamemanager.doll);
        maincamara.SetActive(!gamemanager.doll);
            canvas.SetActive(!gamemanager.doll);
         all.SetActive(!gamemanager.doll);
        a = false;
    }
    void aaa()
    {
        scene1.SetActive(false);
        maincamara.SetActive(!gamemanager.doll);
        canvas.SetActive(!gamemanager.doll);
         all.SetActive(!gamemanager.doll);
    }
   public static void finish()
    {
        gamemanager.doll = false;
      

     }
}
