using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunManager : MonoBehaviour
{
    public float time;
    public GameObject bullet;
    public float bulletspeed;
    public float bulletscale;
    public float bulletDeathTime;
    public float bulletAmount;
    public float bulletFrequency;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Shoot", time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn() //Éú³É×Óµ¯
    {
        if (i < bulletAmount)
        {
            i++;
            GameObject go = (GameObject)Instantiate(bullet);
            go.transform.localScale = new Vector3(bulletscale, bulletscale);
            BulletManager b = go.GetComponent<BulletManager>();
            b.setSpeed(bulletspeed);
            b.time = bulletDeathTime;
            go.transform.localPosition = this.transform.position;
            Invoke("Spawn", bulletFrequency);
        }
    }

    void Shoot()
    {
        i = 0;
        Invoke("Spawn", bulletFrequency);
        Invoke("Shoot", time);
    }

}
