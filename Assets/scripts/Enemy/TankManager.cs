using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    public float ShellShootTime;
    public float ShellScale;
    public float ShellSpeed;
    public float ShellDeathTime;
    public float WarningTime;
    public float WarningFrequency;
    Transform WarningObject;
    bool IfWarning = false;
    public float MachineGunShootTime;
    public float bulletspeed;
    public float bulletscale;
    public float bulletDeathTime;
    public float bulletAmount;
    public float bulletFrequency;
    public float bulletShootTime;

    // Start is called before the first frame update
    void Start()
    {
        WarningObject = GetChild(this.transform, "Warning");
        Transform Emitter1 = GetChild(this.transform, "Emitter1");
        Transform Emitter2 = GetChild(this.transform, "Emitter2");
        Transform MachineGun = GetChild(this.transform, "MachineGun");
        EmitterManager EmitterManager1 = Emitter1.GetComponent<EmitterManager>();
        EmitterManager EmitterManager2 = Emitter2.GetComponent<EmitterManager>();
        EmitterManager1.shellspeed = ShellSpeed;
        EmitterManager1.shellDeathTime = ShellDeathTime;
        EmitterManager1.shellscale = ShellScale;
        EmitterManager1.time = ShellShootTime;
        EmitterManager2.shellspeed = ShellSpeed;
        EmitterManager2.shellDeathTime = ShellDeathTime;
        EmitterManager2.shellscale = ShellScale;
        EmitterManager2.time = ShellShootTime;
        MachineGunManager machineGunManager = MachineGun.GetComponent<MachineGunManager>();
        machineGunManager.time = MachineGunShootTime;
        machineGunManager.bulletAmount = bulletAmount;
        machineGunManager.bulletDeathTime = bulletDeathTime;
        machineGunManager.bulletFrequency = bulletFrequency;
        machineGunManager.bulletscale = bulletscale;
        machineGunManager.bulletspeed = bulletspeed;
        machineGunManager.time = bulletShootTime;
        Invoke("Warning", ShellShootTime - WarningTime);
    }

    // Update is called once per frame
    void Update()
    {

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

    void Warning()
    {
        IfWarning = true;
        Invoke("OpenWarning", WarningFrequency);
        Invoke("CloseWarning", WarningTime);
        Invoke("StopWarning", WarningTime);
        Invoke("Warning", ShellShootTime);
    }
    void OpenWarning()
    {
        if (IfWarning)
        {
            WarningObject.gameObject.SetActive(true);
            Invoke("CloseWarning", WarningFrequency);
        }
    }
    void CloseWarning()
    {
        if (IfWarning)
        {
            WarningObject.gameObject.SetActive(false);
            Invoke("OpenWarning", WarningFrequency);
        }
    }

    void StopWarning()
    {
        IfWarning = false;
    }

}
