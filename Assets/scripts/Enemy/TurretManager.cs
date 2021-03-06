using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public float Deathtime;
    public GameObject dead;
    bool isDead;
    public GameObject DeathAudio;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDead)
        {
            
            //Debug.Log("1");
            playermove2 ifplayer = other.collider.GetComponent<playermove2>();
            if (ifplayer != null)
            {
                GameObject.Find("ShakeCameraManager").GetComponent<Tools>().ShakeScreen(0.05f, 0.01f);
                transform.GetComponent<Ai_Move_main>().enabled = false;
                transform.GetComponent<SpikedCircleManager>().enabled = false;
                Destroy(transform.GetComponent<Rigidbody2D>());
                isDead = true;
                Death1();
            }
        }
    }

    public void Death1()
    {
        GameObject go = (GameObject)Instantiate(dead);
        go.transform.localScale = this.transform.localScale;
        go.transform.localPosition = this.transform.position;
        go.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        GameObject Audio = (GameObject)Instantiate(DeathAudio);
        Audio.transform.localPosition = this.transform.position;
        Transform boom = GetChild(this.transform, "Boom");
        boom.gameObject.SetActive(true);
        //Debug.Log("2");
        Invoke("Death2", Deathtime);
    }

    public void Death2()
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
