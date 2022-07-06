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
    public float Deathtime;
    bool death = false;
    public GameObject dead;

    // Start is called before the first frame update
    void Start()
    {
        death = false;
    }

    // Update is called once per frame
    void Update()
    {
        speed += acceleratedSpeed;
        if (!death)
        {
            Move();
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        //Debug.Log("1");
        playermove2 ifplayer = other.collider.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            Death1();
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

    public void Death1()
    {
        death = true;
        GameObject go = (GameObject)Instantiate(dead);
        go.transform.localScale = this.transform.localScale;
        go.transform.localPosition = this.transform.position;
        Transform boom = GetChild(this.transform, "Boom");
        boom.gameObject.SetActive(true);
        //Debug.Log("2");
        Invoke("Death2", Deathtime);
    }

    public void Death2()//小兵死亡操作
    {
        Destroy(gameObject);
    }

    public static Transform GetChild(Transform parentTF, string childName)
    {
        //在子物体中查找
        Transform childTF = parentTF.Find(childName);

        if (childTF != null)
        {
            return childTF;
        }
        //将问题交由子物体
        int count = parentTF.childCount;
        for (int i = 0; i < count; i++)
        {
            childTF = GetChild(parentTF.GetChild(i), childName);
            if (childTF != null)
            {
                return childTF;
            }
        }
        return null;
    }

}
