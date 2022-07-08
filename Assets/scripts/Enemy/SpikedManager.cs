using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedManager : MonoBehaviour
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
        GameObject obj = collision.gameObject;
        playermove2 b = obj.GetComponent<playermove2>();
        if (b != null)
        {BulletManager.i--;
            //Debug.Log("1");
            GameObject.Find("ShakeCameraManager").GetComponent<Tools>().ShakeScreen(0.2f, 0.01f);
            //Debug.Log("has collision");
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y) * a);//a是力的大小
        }
    }

}
