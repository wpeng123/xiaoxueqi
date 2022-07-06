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

    public float MachineGunShootTime;
    public float bulletspeed;
    public float bulletscale;
    public float bulletDeathTime;
    public float bulletAmount;
    public float bulletFrequency;

    public float MoveTime;
    public float MoveSpeed;

    public int DestoryedWeekness;

    public GameObject dead;

    Transform WarningObject;
    Transform TankTrackVertical1;
    Transform TankTrackHorizontal1;
    Transform TankMachineGun1;
    Transform TankBarrel1;

    Animator MoveAniX;
    Animator MoveAniY;
    Animator ShellShoot;
    Animator BulletShoot;

    float MoveX;
    float MoveY;
    bool IfWarning = false;
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        TankMachineGun1 = GetChild(this.transform, "TankMachineGun1");
        TankTrackVertical1 = GetChild(this.transform, "TankTrackVertical1");
        TankTrackHorizontal1 = GetChild(this.transform, "TankTrackHorizontal1");
        WarningObject = GetChild(this.transform, "Warning");
        TankBarrel1 = GetChild(this.transform, "TankBarrel1");

        Transform Emitter1 = GetChild(this.transform, "Emitter1");
        Transform Emitter2 = GetChild(this.transform, "Emitter2");
        Transform MachineGun = GetChild(this.transform, "MachineGun");

        MoveAniX = TankTrackHorizontal1.GetComponent<Animator>();
        MoveAniY = TankTrackVertical1.GetComponent<Animator>();
        BulletShoot = TankMachineGun1.GetComponent<Animator>();
        ShellShoot = TankBarrel1.GetComponent<Animator>();

        TowerManager EmitterManager1 = Emitter1.GetComponent<TowerManager>();
        EmitterManager1.bulletspeed = ShellSpeed;
        EmitterManager1.bulletDeathTime = ShellDeathTime;
        EmitterManager1.bulletscale = ShellScale;
        EmitterManager1.time = ShellShootTime;
        EmitterManager1.amount = 1;
        EmitterManager1.rotationSpeed = 0;

        TowerManager EmitterManager2 = Emitter2.GetComponent<TowerManager>();
        EmitterManager2.bulletspeed = ShellSpeed;
        EmitterManager2.bulletDeathTime = ShellDeathTime;
        EmitterManager2.bulletscale = ShellScale;
        EmitterManager2.time = ShellShootTime;
        EmitterManager2.amount = 1;
        EmitterManager2.rotationSpeed = 0;

        TowerManager machineGunManager = MachineGun.GetComponent<TowerManager>();
        machineGunManager.time = MachineGunShootTime;
        machineGunManager.amount = bulletAmount;
        machineGunManager.bulletDeathTime = bulletDeathTime;
        machineGunManager.spawnfrequency = bulletFrequency;
        machineGunManager.bulletscale = bulletscale;
        machineGunManager.bulletspeed = bulletspeed;
        machineGunManager.rotationSpeed = 0;

        GetDirection2();
        Invoke("AniMGunStart", MachineGunShootTime);
        Invoke("AniShellShootStart", ShellShootTime - 3.0f);
        Invoke("Warning", ShellShootTime - WarningTime);
    }

    // Update is called once per frame
    void Update()
    {
        Transform MachineGun = GetChild(this.transform, "MachineGun");
        Transform head = GetChild(this.transform, "TankSpinningPlatform");
        Transform Emitter1 = GetChild(this.transform, "Emitter1");
        Transform Emitter2 = GetChild(this.transform, "Emitter2");
        TowerManager EmitterManager1 = Emitter1.GetComponent<TowerManager>();
        TowerManager EmitterManager2 = Emitter2.GetComponent<TowerManager>();
        TowerManager machineGunManager = MachineGun.GetComponent<TowerManager>();
        EmitterManager1.defaultZ = head.eulerAngles.z;
        EmitterManager2.defaultZ = head.eulerAngles.z;
        machineGunManager.defaultZ = head.eulerAngles.z;

        Move();

        if(DestoryedWeekness == 3) 
        {
            Death1();
        }

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

    void Move()
    {
        if (!isDead)
        {
            Vector2 position = transform.position;
            position.x = transform.position.x + MoveX * MoveSpeed * Time.deltaTime;
            position.y = transform.position.y + MoveY * MoveSpeed * Time.deltaTime;
            transform.position = position;
        }
    }

    void GetDirection()
    {
        int i = Random.Range(0, 5);
        switch (i)
        {
            case 1:
                MoveAniX.SetBool("Horizontal", true);
                MoveAniY.SetBool("Vertical", false);
                MoveX = 1;
                MoveY = 0;
                break;
            case 2:
                MoveAniX.SetBool("Horizontal", true);
                MoveAniY.SetBool("Vertical", false);
                MoveX = -1;
                MoveY = 0;
                break;
            case 3:
                MoveAniX.SetBool("Horizontal", false);
                MoveAniY.SetBool("Vertical", true);
                MoveX = 0;
                MoveY = 1;
                break;
            case 4:
                MoveAniX.SetBool("Horizontal", false);
                MoveAniY.SetBool("Vertical", true);
                MoveX = 0;
                MoveY = -1;
                break;
        }
        if (transform.position.y < -8)
        {
            MoveX = 0;
            MoveY = 1;
        }
        else if (transform.position.x > 22)
        {
            MoveX = -1;
            MoveY = 0;
        }
        else if (transform.position.x < -22)
        {
            MoveX = 1;
            MoveY = 0;
        }
        else if (transform.position.y > 8)
        {
            MoveX = 0;
            MoveY = -1;
        }

    }

    void GetDirection2()
    {
        GetDirection();
        Invoke("GetDirection2", MoveTime);
    }

    void AniMGunStart()
    {
        BulletShoot.SetBool("MGun", true);
        Invoke("AniMGunStop", bulletAmount * bulletFrequency);
    }
    void AniMGunStop()
    {
        BulletShoot.SetBool("MGun", false);
        Invoke("AniMGunStart", MachineGunShootTime- bulletAmount * bulletFrequency);
    }

    void AniShellShootStart()
    {
        ShellShoot.SetBool("Shoot", true);
        Invoke("AniShellShootStop", 3.0f);
    }
    void AniShellShootStop()
    {
        ShellShoot.SetBool("Shoot", false);
        Invoke("AniShellShootStart", ShellShootTime - 3.0f);
    }

    public void Death1()
    {
        isDead = true;
        GameObject go = (GameObject)Instantiate(dead);
        go.transform.localScale = this.transform.localScale;
        go.transform.localPosition = this.transform.position;
        Transform boom = GetChild(this.transform, "Boom");
        boom.gameObject.SetActive(true);
        //Debug.Log("2");
        Invoke("Death2", 0.4f);
    }

    public void Death2()
    {
        Destroy(gameObject);
    }

}
