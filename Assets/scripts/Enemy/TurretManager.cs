using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public float Deathtime;
    public GameObject dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        //Debug.Log("1");
        playermove2 ifplayer = other.collider.GetComponent<playermove2>();
        if (ifplayer != null)
        {

            Death1();
        }
    }

    public void Death1()
    {
        GameObject go = (GameObject)Instantiate(dead);
        go.transform.localScale = this.transform.localScale;
        go.transform.localPosition = this.transform.position;
        Transform boom = GetChild(this.transform, "Boom");
        boom.gameObject.SetActive(true);
        //Debug.Log("2");
        Invoke("Death2", Deathtime);
    }

    public void Death2()//С����������
    {
        Destroy(gameObject);
    }

    public static Transform GetChild(Transform parentTF, string childName)
    {
        //���������в���
        Transform childTF = parentTF.Find(childName);

        if (childTF != null)
        {
            return childTF;
        }
        //�����⽻��������
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
