using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject ShakeCamera;
    float ShakeTime;
    float ShakeFrequency;
    bool shake;
    public void ShakeScreen(float time,float frequency)
    {
        //Debug.Log("2");
        shake = true;
        ShakeTime = time;
        ShakeFrequency = frequency;
        Invoke("ToCameraA",frequency);
        Invoke("ShakeEnd", time);
    }
    // Start is called before the first frame update
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

    void ToCameraA()
    {
        MainCamera.SetActive(true);
        ShakeCamera.SetActive(false);
        //ShakeCamera.GetComponent<Camera>().enabled = false;
        //ShakeCamera.GetComponent<AudioListener>().enabled = false;
        if (shake)
        { 
            Invoke("ToCameraB", ShakeFrequency);
        }
    }

    void ToCameraB()
    {
        if (shake)
        {
            MainCamera.SetActive(false);
            ShakeCamera.SetActive(true);
            //ShakeCamera.GetComponent<Camera>().enabled = true;
            //ShakeCamera.GetComponent<AudioListener>().enabled = true;
            Invoke("ToCameraA", ShakeFrequency);
        }
    }

    void ShakeEnd()
    {
        shake = false;
        Invoke("ToCameraA", ShakeFrequency);
    }

    void Update()
    {
        Vector3 position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,MainCamera.transform.position.z);
        ShakeCamera.transform.position = position;
    }

}
