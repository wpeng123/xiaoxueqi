using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove2 : MonoBehaviour
{
    public float minenergy;
    public float dump;
    public float maxpower;
    public static float health;
    float speed;
    float maxspeed;
    float energy;
    float basespeed;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().drag = dump;
        maxspeed=maxpower/dump;
        energy = minenergy;
        basespeed = maxpower / minenergy;
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            //gamemanager.instance.UpdateGameState(gamemanager.Gamestate.Defeat);
        }
        speed = Mathf.Sqrt(Mathf.Pow(this.GetComponent<Rigidbody2D>().velocity.x, 2) + Mathf.Pow(this.GetComponent<Rigidbody2D>().velocity.y, 2));
        //LookAt2D(this.GetComponent<Rigidbody2D>().velocity);
        Tractive_force();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float angle = Mathf.Atan2(vertical, horizontal);
        float toward = Mathf.Sqrt(Mathf.Pow(horizontal, 2) + Mathf.Pow(vertical, 2));
        if (!checkoverspeed())
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * toward * energy);
            //Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
            //Debug.Log(speed);
            //string str = "h:" + horizontal + " v:" + vertical;
            //Debug.Log(str);
        }
        else
        {
            //this.GetComponent<Rigidbody2D>().AddForce(-this.GetComponent<Rigidbody2D>().drag);
        }
    }
    void OnCollisionEnter2D(Collision2D collisioninfo)
    {
        //if (speed > 8.0)
        //{
        //    if (collisioninfo.gameObject.tag == "enemy")
        //    {
        //        Debug.Log("destory");
        //        Destroy(collisioninfo.gameObject);
        //        checkover();
        //    }
        //}

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (speed > 8.0)
        {
            if (collision.gameObject.tag == "enemy")
            {
                Debug.Log("destory");
                Destroy(collision.gameObject.transform.parent.gameObject,0.1f);
                checkover();
            }
        }
    }
    public Quaternion targetangle()
    {
        Vector2 dir = this.GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward); 
    }
    void LookAt2D(Vector2 dir)
     {
         float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
         transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
     }
    bool checkoverspeed()
    {
        if (speed <= maxspeed)
            return false;
        else
            return true;
    }
    void Tractive_force()
    {
        if (speed<basespeed) { energy = minenergy; }
        else
        {
            energy = maxpower / speed;
        }

    }
    public float getspeed()
    {
        return speed;
    }

    bool checkover()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log("enemy:"+gos.Length);
        return true;
    }
}
