using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHeadManager : MonoBehaviour
{
    private Vector3 targetPosition;
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
        var direction = targetPosition - transform.position;//目标方向

        Vector3 dir = targetPosition - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, dir, Vector3.forward);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle), rotationSpeed);
    }

}
