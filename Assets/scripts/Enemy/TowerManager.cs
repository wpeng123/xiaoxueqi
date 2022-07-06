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
    public float rotationSpeed = 100;
    public float defaultZ;
    int x;
    // Start is called before the first frame update
    void Start()
    {
        defaultZ = 0;
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
            b.rotationSpeed = rotationSpeed;
            if (defaultZ != 0)
            {
                b.defaultZ = defaultZ;
            }
            go.transform.localPosition = this.transform.position;
            Invoke("Spawn", spawnfrequency);
        }
    }

    public void Spawn2() //生成子弹
    {
        if (time != 0)
        {
            x = 0;
            Spawn();
            Invoke("Spawn2", time);
        }
    }

    public void Spawn3() //生成子弹
    {
        x = 0;
        Spawn();
    }

}
