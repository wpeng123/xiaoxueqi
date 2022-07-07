using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedCircleManager : MonoBehaviour
{
    private Vector3 targetPosition;
    public float speed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        targetPosition = GameObject.Find("MechaBall").transform.position;
        var direction = targetPosition - transform.position;//Ŀ�귽��
        
        //transform.Translate(direction.normalized * speed * Time.deltaTime * 0.5f, Space.World);//��Ŀ�귽���ƶ���normalized��һʵ�������ƶ�

        Transform child = GetChild(this.transform, "direction");
        var directionself = child.position - transform.position;//������
        Vector3 V1 = directionself.normalized;                  //������
        Vector3 V2 = direction.normalized;                      //Ŀ�곯��
        //�Ƕ�
        float z1;

        Vector3 result = Vector3.Cross(V1, V2);
        if (result.z > 0)
        {
            z1 = V1.z;
            this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z + rotationSpeed);
        }
        else
        {
            z1 = V1.z;
            this.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z - rotationSpeed);
        }

        transform.position += transform.up * speed * Time.deltaTime;

    }


    public static Transform GetChild(Transform parentTF, string childName)
    {
        //���������в���
        Transform childTF = parentTF.Find(childName);

        if (childTF != null)
        {
            return childTF;
        }
        //�����⽻��������
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
