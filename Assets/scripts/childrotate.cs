using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childrotate : MonoBehaviour
{
    float target = 0;
    public GameObject obj;
    Quaternion qua;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 dir = this.GetComponentInParent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        qua = obj.GetComponent<playermove2>().targetangle();
        
        if (transform.rotation.eulerAngles.z - angle_to_eulerangle(angle) <= 180 && transform.rotation.eulerAngles.z - angle_to_eulerangle(angle) > 0)
        {
            Debug.Log(1);
            target = transform.rotation.eulerAngles.z - angle_to_eulerangle(angle)>speed?transform.rotation.eulerAngles.z - speed: angle_to_eulerangle(angle);
        }
        else if (transform.rotation.eulerAngles.z - angle_to_eulerangle(angle) > 180 || transform.rotation.eulerAngles.z - angle_to_eulerangle(angle) < 0)
        {
            Debug.Log(2);
            target = ((transform.rotation.eulerAngles.z - angle_to_eulerangle(angle) < (-speed)|| transform.rotation.eulerAngles.z - angle_to_eulerangle(angle)>360-speed) ? transform.rotation.eulerAngles.z + speed : angle_to_eulerangle(angle))% 360 ;
        }
        else { target = transform.rotation.eulerAngles.z; }
        this.transform.rotation = Quaternion.AngleAxis(eulerangle_to_angle(target), Vector3.forward);
    }

    float angle_to_eulerangle(float angle)
    {
        return angle >= 0 ? angle : 360 + angle;
    }

    float eulerangle_to_angle(float eulerangle)
    {
        return eulerangle <= 180 ? eulerangle : eulerangle - 360;
    }

    public float gettarget()
    {
        return target;
    }
}
