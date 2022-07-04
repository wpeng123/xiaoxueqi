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
        var direction = targetPosition - transform.position;//Ŀ�귽��
        transform.Translate(direction.normalized * speed * Time.deltaTime * 0.5f, Space.World);//��Ŀ�귽���ƶ���normalized��һʵ�������ƶ�

        Vector3 dir = targetPosition - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle), rotationSpeed);
    }

    public void Death()//С����������
    {
        Destroy(gameObject);
    }
}
