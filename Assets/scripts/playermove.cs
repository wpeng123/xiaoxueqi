using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public float energy;
    public float dump;
    public float maxspeed;
    float speed;
    float power;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().drag = dump;
        power = maxspeed * dump;
    }

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Sqrt(Mathf.Pow(this.GetComponent<Rigidbody2D>().velocity.x, 2) + Mathf.Pow(this.GetComponent<Rigidbody2D>().velocity.y, 2));
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float angle=Mathf.Atan2(vertical,horizontal);
        float toward = Mathf.Sqrt(Mathf.Pow(horizontal, 2) + Mathf.Pow(vertical, 2));
        if (!checkoverspeed())
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) *toward* energy);
            Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
            //string str = "h:" + horizontal + " v:" + vertical;
            //Debug.Log(str);
        }
        else
        {
            //this.GetComponent<Rigidbody2D>().AddForce(-this.GetComponent<Rigidbody2D>().drag);
        }
    }
    bool checkoverspeed()
    {
        if (speed <= maxspeed)
            return false;
        else
            return true;
    }
    public float getspeed()
    {
        return speed;
    }

}
