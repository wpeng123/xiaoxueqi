using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour
{
    private Vector3 targetPosition;
    public float speed;
    public float time;
    public float rotationSpeed = 100f;
    public float damage;
    public float defaultZ;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("MechaBall").transform.position;
        Vector3 dir = targetPosition - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
        
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle), rotationSpeed);

        if (defaultZ != 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, defaultZ);
        }

        Invoke("death", time);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        playermove2 ifplayer = other.collider.GetComponent<playermove2>();
        if (ifplayer != null)
        {

            death();
        }
    }

    void death()
    {
        Destroy(gameObject);
    }

    public void setSpeed(float a)
    {
        speed = a;
    }


    private void Move()
    {

        transform.position += transform.up * speed * Time.deltaTime;
    }

}
