using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsManager : MonoBehaviour
{

    public float power;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float a = power;
        GameObject obj = this.transform.parent.gameObject;
        playermove2 b = collision.gameObject.GetComponent<playermove2>();
        if (b != null)
        {
            //Debug.Log("has collision");
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(obj.transform.position.x - collision.gameObject.transform.position.x, obj.transform.position.y - collision.gameObject.transform.position.y) * a);//a是力的大小
        }
    }
}
