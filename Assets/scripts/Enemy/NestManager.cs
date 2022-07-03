using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject CircleEnemy;
    public float time; //生成间隔
    public float speed;
    public float rotationSpeed;
    public float amount;//生成数量


    void Start()
    {
        Invoke("Spawn2", time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn() //生成小兵
    {
        GameObject go = (GameObject)Instantiate(CircleEnemy);
        go.GetComponent<CircleEnemyManager>().speed = speed;
        go.GetComponent<CircleEnemyManager>().rotationSpeed = rotationSpeed;
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
    }

    void Spawn2() //生成操作
    {
        for (int i = 0; i < amount; i++)
        {
            Spawn();
        }
        Invoke("Spawn2", time);
    }

    void OnTriggerEnter2D(Collider2D other)//接触时触发，无需调用
    {
        playermove2 ifplayer = other.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            Death();
        }
    }

    void Death() //死亡（播放动画以及死亡）
    {
        Destroy(gameObject);
        ChildDeath(this.gameObject);
    }

    void ChildDeath(GameObject child)
    {
        for (int c = 0; c < child.transform.childCount; c++)
        {
            CircleEnemyManager CircleEnemyDeath = child.transform.GetChild(c).GetComponent<CircleEnemyManager>();
            CircleEnemyDeath.Death();
        }
    }
}
