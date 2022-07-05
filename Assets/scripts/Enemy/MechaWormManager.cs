using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWormManager : MonoBehaviour
{
    Animator ani;
    public float aniTime;
    public float Deathtime;
    public GameObject dead;
    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();

        Invoke("animate",3.5f);
        
        //Debug.Log(ani);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void animate()
    {
        animationStart();
        Invoke("animationStop", 0.5f);
        Invoke("animate", aniTime);

    }

    void animationStart()
    {
        ani.SetBool("Release",true);
    }

    void animationStop()
    {
        ani.SetBool("Release", false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        //Debug.Log("1");
        playermove2 ifplayer = other.collider.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            Death1();
            //Debug.Log("2");
            Invoke("Death2", Deathtime);
        }
    }

    void Death1()
    {
        Transform child = GetChild(this.transform, "Spawner");
        child.GetComponent<NestManager>().Death();
        Transform boom = GetChild(this.transform, "Boom");
        boom.gameObject.SetActive(true);
        GameObject go = (GameObject)Instantiate(dead);
        go.transform.localScale = this.transform.localScale;
        go.transform.localPosition = this.transform.position;
    }

    void Death2() //死亡（播放动画以及死亡）
    {
        Destroy(gameObject);
    }

    public static Transform GetChild(Transform parentTF, string childName)
    {
        //在子物体中查找
        Transform childTF = parentTF.Find(childName);

        if (childTF != null)
        {
            return childTF;
        }
        //将问题交由子物体
        int count = parentTF.childCount;
        for (int i = 0; i < count; i++)
        {
            childTF = GetChild(parentTF.GetChild(i), childName);
            if (childTF != null)
            {
                return childTF;
            }
        }
        return null;
    }

}
