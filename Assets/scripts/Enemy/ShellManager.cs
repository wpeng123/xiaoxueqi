using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellManager : MonoBehaviour
{
    private Vector3 targetPosition;
    public float speed;
    public float time;
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("MechaBall").transform.position;
        Vector3 dir = targetPosition - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle), rotationSpeed);
        Invoke("death", time);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void OnTriggerStay2D(Collider2D other) //Õ¨µ¯Åöµ½Íæ¼Ò
    {
        playermove2 ifplayer = other.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            death();
        }
    }

    void death()
    {
        Destroy(gameObject);
    }
    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
