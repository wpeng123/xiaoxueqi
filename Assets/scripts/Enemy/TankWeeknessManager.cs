using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tools;

public class TankWeeknessManager : MonoBehaviour
{
    public GameObject Broken;
    bool isBroken;
    // Start is called before the first frame update
    void Start()
    {
        isBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isBroken)
        {
            //Debug.Log("1");
            playermove2 ifplayer = other.collider.GetComponent<playermove2>();
            if (ifplayer != null)
            {
                isBroken = true;
                Death1();
            }
        }
    }

    public void Death1()
    {
        Transform boom = GetChild(this.transform, "Boom");
        boom.gameObject.SetActive(true);
        //Debug.Log("2");
        Invoke("Death2", 0.4f);
    }

    public void Death2()
    {
        Broken.SetActive(true);

        TankManager Tank = this.transform.parent.GetComponent<TankManager>();
        Tank.DestoryedWeekness++;

        this.gameObject.SetActive(false);
    }

}
