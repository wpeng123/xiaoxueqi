using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tools;

public class TankWeeknessManager : MonoBehaviour
{
    public GameObject Broken;
    public GameObject DeathAudio;
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
                GameObject.Find("ShakeCameraManager").GetComponent<Tools>().ShakeScreen(0.2f, 0.01f);
                isBroken = true;
                Death1();
            }
        }
    }

    public void Death1()
    {
        GameObject Audio = (GameObject)Instantiate(DeathAudio);
        Audio.transform.localPosition = this.transform.position;
        transform.GetComponent<Collider2D>().enabled = false;
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
