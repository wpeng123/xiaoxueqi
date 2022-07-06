using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    
    public static void ShakeScreen(float time,float frequency)
    {
        GameObject MainCamera;
        GameObject ShakeCamera;
        MainCamera = GameObject.Find("Main Camera");
        ShakeCamera = GameObject.Find("ShakeCamera");    
        for(int i = 0; i < time; i++)
        {
            new WaitForSeconds(1);
            Debug.Log(i);
        }
    }
    // Start is called before the first frame update
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

    //public void ToCameraA(GameObject A, GameObject B,float frequency)
    //{
    //    A.SetActive(true);
    //    B.SetActive(false);
    //    Invoke("ToCameraB", frequency);
    //}

}
