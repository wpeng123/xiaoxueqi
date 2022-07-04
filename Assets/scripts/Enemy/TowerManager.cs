using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    public float time;
    public GameObject bullet;
    public float bulletspeed;
    public float bulletscale;
    public float bulletDeathTime;
    public float amount;
    public float spawnfrequency;
    int x;
    // Start is called before the first frame update
    void Start()
    {
        x = 0;
        Invoke("Spawn2", time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn() //生成子弹
    {
        if (x < amount)
        {
            x++;
            GameObject go = (GameObject)Instantiate(bullet);
            go.transform.localScale = new Vector3(bulletscale, bulletscale);
            BulletManager b = go.GetComponent<BulletManager>();
            b.setSpeed(bulletspeed);
            b.time = bulletDeathTime;
            go.transform.localPosition = this.transform.position;
            Invoke("Spawn", spawnfrequency);
        }
    }

    void Spawn2() //生成子弹
    {
        x = 0;
        Spawn();
        Invoke("Spawn2", time);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        playermove2 ifplayer = other.collider.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            Debug.Log("1");
            Death();
        }
    }

    void Death() //死亡（播放动画以及死亡）
    {
        Destroy(gameObject);
    }

}
