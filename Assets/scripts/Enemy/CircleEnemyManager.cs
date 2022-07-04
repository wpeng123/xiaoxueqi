using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleEnemyManager : MonoBehaviour
{
    private Vector3 targetPosition;
    public float speed;
    public float rotationSpeed;
    public float acceleratedSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        speed += acceleratedSpeed;
        Move();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        playermove2 ifplayer = other.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            Death();
        }
    }

    private void Move()
    {
        targetPosition = GameObject.Find("MechaBall").transform.position;
        var direction = targetPosition - transform.position;//目标方向
        transform.Translate(direction.normalized * speed * Time.deltaTime * 0.5f, Space.World);//向目标方向移动，normalized归一实现匀速移动

        Vector3 dir = targetPosition - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle), rotationSpeed);
    }

    public void Death()//小兵死亡操作
    {
        Destroy(gameObject);
    }
}
