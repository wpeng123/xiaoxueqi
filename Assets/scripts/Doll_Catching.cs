using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Doll_Catching : MonoBehaviour
{
    Doll_Catching d;

    public GameObject scene1;
    public static GameObject c; 
    public GameObject maincamara;
    public GameObject canvas;
    public GameObject upgradefinish;
    public GameObject all;
    public static bool a=false;
    public static bool b = false;
    static bool doll;

    // Start is called before the first frame update
    private void Start()
    {
        d = this;
        c = scene1;
    }

    // Update is called once per frame
    void Update()
    {
        doll = (gamemanager.instance.state == gamemanager.Gamestate.Prize_Clawing);
        a = doll;
        if(a)
            scene1.SetActive(doll);
        maincamara.SetActive(!doll);
            canvas.SetActive(!doll);
         all.SetActive(!doll);
        a = false;
    }
    void aaa()
    {
        scene1.SetActive(false);
        maincamara.SetActive(!doll);
        canvas.SetActive(!doll);
         all.SetActive(!doll);
    }
   public static void finish()
   {
        doll = false;
        Doll_Catching.c.SetActive(false);
        gamemanager.instance.UpdateGameState(gamemanager.Gamestate.Playing);
   }
}
