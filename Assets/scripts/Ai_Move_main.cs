using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Situation
{
    chase, attack, dead, normal, retreat
}
public struct Ai_variable
{//Ai脚本需要的两个数值变量
    public Situation situation;

    public float position_x;//当前target所在位置
    public float position_y;
    public float position_z;

    public float distance;
    public float attack_distance_0;
    public float attack_distance_1;

    // float distance_x;//ai与目标之间的距离 直线
    public float distance_apart;//保持距离

    public Vector2 direction;
}
public struct Weight
{
    public float stage_03;
    public float stage_05;//权重
    public float stage_10;
    public float stage_20;
    public float stage_40;
}

public class Ai_Move_main : MonoBehaviour
{
    // Start is called before the first frame update
    Time time;
    public Weight weight;
    public Ai_variable enemy_stru;
    public Ai_variable target_stru;
    public Rigidbody2D enemy;//获取敌人以及敌人目标
    public GameObject target;
    public int counter;//用于记录球体运动情况 被卡住time

    public float nearest_attack_distance;
    public float farthest_attack_distance;

    public float retrear_speed_weight;

    public float bulletspeed;
    public float bulletscale;
    public float bulletDeathTime;
    public float bulletAmount;
    public float bulletFrequency;

    public float attack_interval_time;

    private int is_attack;//是否攻击
    private Transform attack1;
    private Transform attack2;


    void set_attack()
    {
        is_attack = 1;
    }
    void initialie()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        is_attack = 1;
        if (attack_interval_time == 0)
        {
            attack_interval_time = 4;
        }

        if (enemy.tag == "enemy_shooter")
        {
            attack1 = GetChild(this.transform, "BulletSpawner");
            attack2 = GetChild(this.transform, "BulletSpawner (1)");
            if (nearest_attack_distance != 0)
            {
                enemy_stru.attack_distance_0 = nearest_attack_distance;
            }
            else
            {
                enemy_stru.attack_distance_0 = 4;
            }
            if (farthest_attack_distance != 0)
            {
                enemy_stru.attack_distance_1 = farthest_attack_distance;
            }
            else
            {
                enemy_stru.attack_distance_1 = 10;
            }

            enemy_stru.distance_apart = enemy_stru.attack_distance_0;
        }
        else if (enemy.tag == "enemy_spider")
        {
            attack1 = GetChild(enemy.transform, "spawner");

            if (nearest_attack_distance != 0)
            {
                enemy_stru.attack_distance_0 = nearest_attack_distance;
            }
            else
            {
                enemy_stru.attack_distance_0 = 4;
            }
            if (farthest_attack_distance != 0)
            {
                enemy_stru.attack_distance_1 = farthest_attack_distance;
            }
            else
            {
                enemy_stru.attack_distance_1 = 10;
            }
            enemy_stru.distance_apart = enemy_stru.attack_distance_0;
        }
        else
            enemy_stru.distance_apart = 0.1f;//enemy需要与target之间保持的距离

        //不同距离移动速度的权值
        weight.stage_03 = 7.00f;
        weight.stage_05 = 5.50f;
        weight.stage_10 = 5.00f;
        weight.stage_20 = 4.50f;
        weight.stage_40 = 0.10f;



        enemy_stru.situation = Situation.chase;//当前的情况
        if (enemy.tag == "enemy_shooter")
        {
            TowerManager machineGunManager = attack1.GetComponent<TowerManager>();
            machineGunManager.amount = bulletAmount;
            machineGunManager.bulletDeathTime = bulletDeathTime;
            machineGunManager.spawnfrequency = bulletFrequency;
            machineGunManager.bulletscale = bulletscale;
            machineGunManager.bulletspeed = bulletspeed;
            machineGunManager.rotationSpeed = 0;

            TowerManager machineGunManager2 = attack2.GetComponent<TowerManager>();
            machineGunManager2.amount = bulletAmount;
            machineGunManager2.bulletDeathTime = bulletDeathTime;
            machineGunManager2.spawnfrequency = bulletFrequency;
            machineGunManager2.bulletscale = bulletscale;
            machineGunManager2.bulletspeed = bulletspeed;
            machineGunManager2.rotationSpeed = 0;
        }
        else if (enemy.tag == "enemy_spider")
        {
            TowerManager machineGunManager = attack1.GetComponent<TowerManager>();
            machineGunManager.amount = bulletAmount;
            machineGunManager.bulletDeathTime = bulletDeathTime;
            machineGunManager.spawnfrequency = bulletFrequency;
            machineGunManager.bulletscale = bulletscale;
            machineGunManager.bulletspeed = bulletspeed;

        }



    }
    void attack()
    {


        if (is_attack == 1)
        {
           // Debug.Log(enemy.tag+enemy.tag + " 攻击！！");
            enemy.velocity = Vector2.zero;
            if (enemy.tag == "enemy_shooter")
            {
                Animator spider_chase = GetChild(enemy.transform, "ArmouredCarShoot1").GetComponent<Animator>();
                spider_chase.SetBool("Shoot", true);
                TowerManager attack3 = attack1.GetComponent<TowerManager>();
                TowerManager attack4 = attack2.GetComponent<TowerManager>();
                attack3.Spawn3();
                attack4.Spawn3();
            }
            else if (enemy.tag == "enemy_spider")
            {
                Animator spider_chase = GetChild(enemy.transform, "TurretShoot1").GetComponent<Animator>();
                spider_chase.SetBool("Shoot", true);
                //Debug.Log(this.tag + "attack");
                TowerManager attack3 = attack1.GetComponent<TowerManager>();
                attack3.Spawn3();
            }
           


            is_attack = -1;
        }
        else if (is_attack == -1)
        {
            is_attack = 0;
            if (enemy.tag == "enemy_shooter")
            {
                Animator spider_chase = GetChild(enemy.transform, "ArmouredCarShoot1").GetComponent<Animator>();
                spider_chase.SetBool("Shoot", false);
              
            }
            else if (enemy.tag == "enemy_spider")
            {
                Animator spider_chase = GetChild(enemy.transform, "TurretShoot1").GetComponent<Animator>();
                spider_chase.SetBool("Shoot", false);
            }
         
            Invoke("set_attack", attack_interval_time);
        }

    }
    Situation get_situation()
    {
        if (enemy.tag == "enemy_shooter" || enemy.tag == "enemy_spider")
        {
            if (enemy_stru.distance > enemy_stru.attack_distance_1)
            {
                return Situation.chase;


            }
            else if (enemy_stru.distance < enemy_stru.attack_distance_1 && enemy_stru.distance > enemy_stru.attack_distance_0)
            {

                return Situation.attack;

            }
            else
            {
                return Situation.retreat;

            }
        }
        else
        {
            /*if (enemy_stru.distance > position_stru.distance_apart)
             {
                 // enemy.AddRelativeForce(getforce());
                 if (position_stru.last_distance < position_stru.distance + 0.05 && position_stru.last_distance > position_stru.distance - 0.05)
                     counter++;
                 if (counter >= 4)
                 {
                     if (situation != Situation.normal)
                     {
                         situation = Situation.normal;
                         counter = 0;
                     }


                     //if()
                 }

                 //enemy.AddForce(getforce());
                 //print("add force" + getforce());
             }
             else
             {
                 enemy.velocity = Vector3.zero;
                 //print("stop");
             }*/
        }
        return Situation.normal;

    }

    bool need_get_situation()
    {
        Situation situation = get_situation();

        if (is_attack == 0)
            return false;
        else
            return true;
    }
    Vector2 getforce()//得到当前力的方向以及权值
    {
        float need_weight = 0;
        if (enemy_stru.distance > 20f)
            need_weight = weight.stage_40;
        else if (enemy_stru.distance > 10f)
            need_weight = weight.stage_20;
        else if (enemy_stru.distance > 5f)
            need_weight = weight.stage_10;

        else if (enemy_stru.distance > 3f)
            need_weight = weight.stage_05;
        else
            need_weight = weight.stage_03;

        float dir_x = (target_stru.position_x - enemy_stru.position_x);
        int sign_x = dir_x >= 0 ? 1 : -1;
        float dir_y = (target_stru.position_y - enemy_stru.position_y);
        int sign_y = dir_y >= 0 ? 1 : -1;

        float abs_dir_x = Mathf.Abs(dir_x);
        float abs_dir_y = Mathf.Abs(dir_y);

        float scale_dir_x = (abs_dir_x / (abs_dir_x + abs_dir_y)) * need_weight * sign_x;
        float scale_dir_y = (abs_dir_y / (abs_dir_x + abs_dir_y)) * need_weight * sign_y;
        Vector2 direction;
        direction.x = scale_dir_x;
        direction.y = scale_dir_y;

        //Debug.Log(direction);
        return direction;
    }
    Vector2 getforce_shooter()//得到当前力的方向以及权值
    {
        float need_weight = 0;
        if (enemy_stru.distance > 20f)
            need_weight = weight.stage_40;
        else if (enemy_stru.distance > 10f)
            need_weight = weight.stage_20;
        else if (enemy_stru.distance > 5f)
            need_weight = weight.stage_10;
        else if (enemy_stru.distance > 3f)
            need_weight = weight.stage_05;
        else
            need_weight = weight.stage_03;

        float dir_x = (target_stru.position_x - enemy_stru.position_x);
        int sign_x = dir_x >= 0 ? 1 : -1;
        float dir_y = (target_stru.position_y - enemy_stru.position_y);
        int sign_y = dir_y >= 0 ? 1 : -1;

        float abs_dir_x = Mathf.Abs(dir_x);
        float abs_dir_y = Mathf.Abs(dir_y);

        float proportion_x = (abs_dir_x / (abs_dir_x + abs_dir_y));
        float proportion_y = (abs_dir_y / (abs_dir_x + abs_dir_y));

        float scale_dir_x = proportion_x * need_weight * sign_x;
        float scale_dir_y = proportion_y * need_weight * sign_y;
        Vector2 direction;
        direction.x = scale_dir_x;
        direction.y = scale_dir_y;

        //Debug.Log(direction);
        if (proportion_x > proportion_y)
        {
            direction.y = 0;
        }
        else
        {
            direction.x = 0;
        }
        return direction;
    }
    float get_distance()//得到enemy以及target的距离
    {
        float enemy_distance_x = enemy.transform.position.x;
        float target_distance_x = target.transform.position.x;
        //Debug.Log(enemy_distance_x+ "  " +target_distance_x);
        float distance_sqre = Mathf.Pow(enemy.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(enemy.transform.position.y - target.transform.position.y, 2);

        return Mathf.Sqrt(distance_sqre);
    }

    void update_information()
    {
        nearest_attack_distance = enemy_stru.attack_distance_0;
        farthest_attack_distance = enemy_stru.attack_distance_1;

        target_stru.position_x = target.transform.position.x;
        target_stru.position_y = target.transform.position.y;
        target_stru.position_z = target.transform.position.z;

        enemy_stru.position_x = enemy.transform.position.x;
        enemy_stru.position_y = enemy.transform.position.y;
        enemy_stru.position_z = enemy.transform.position.z;

        //if(enemy.tag=="enemy_shooter")
        //LookAt2D(target.position);

        enemy_stru.distance = get_distance();
        if (need_get_situation() && enemy.tag == "enemy_spider")
            enemy_stru.situation = get_situation();
        else
        {
            enemy_stru.situation = get_situation();
        }
        // Debug.Log("position_stru.position_x" + position_stru.position_x + " position_stru.position_y" + position_stru.position_y + "position_stru.diatance" + position_stru.distance);
        //Debug.Log("position_stru.diatance" + enemy_stru.distance);
    }
    void Start()
    {
        initialie();
        // enemy.transform;
    }
    private void FixedUpdate()
    {


    }
    // Update is called once per frame
    void Update()
    {

        update_information();
        //Debug.Log(enemy.tag + " " + enemy_stru.situation);
        switch (enemy_stru.situation)
        {
            case Situation.chase:
                {


                    if (enemy.tag == "enemy_spider")
                    {
                        Animator spider_chase = GetChild(enemy.transform, "TurretWalk1").GetComponent<Animator>();
                        spider_chase.SetBool("Walk", true);

                        enemy.velocity = getforce_shooter();
                    }
                    else if (enemy.tag == "enemy_shooter")
                    {
                        Animator spider_chase = GetChild(enemy.transform, "ArmouredCarRun1").GetComponent<Animator>();
                        spider_chase.SetBool("Run", true);
                        enemy.velocity = getforce();
                    }
                    //Debug.Log(enemy.tag + enemy.velocity);
                    break;
                }
            case Situation.attack:
                {
                    if (enemy.tag == "enemy_spider")
                    {
                        Animator spider_chase = GetChild(enemy.transform, "TurretWalk1").GetComponent<Animator>();
                        spider_chase.SetBool("Walk", false);

                       
                    }
                    else if (enemy.tag == "enemy_shooter")
                    {
                        Animator spider_chase = GetChild(enemy.transform, "ArmouredCarRun1").GetComponent<Animator>();
                        spider_chase.SetBool("Run", false);
                        
                    }
                  
                    attack();
                    //
                    break;
                }
            case Situation.dead:
                {
                    //
                    break;
                }
            case Situation.retreat:
                {
                    if (enemy.tag == "enemy_spider")
                    {
                        Animator spider_chase = GetChild(enemy.transform, "TurretWalk1").GetComponent<Animator>();
                        spider_chase.SetBool("Walk", true);

                        enemy.velocity = getforce_shooter();
                    }
                    else if (enemy.tag == "enemy_shooter")
                    {
                        Animator spider_chase = GetChild(enemy.transform, "ArmouredCarRun1").GetComponent<Animator>();
                        spider_chase.SetBool("Run", true);
                        enemy.velocity = getforce();
                    }
                    if (retrear_speed_weight!=0)
                        enemy.velocity = getforce_shooter() * (-retrear_speed_weight);
                    else
                    enemy.velocity = getforce_shooter() * (-0.8f);
                    break;
                }


        }
        //enemy.velocity = Vector2.zero;
        if (enemy.tag == "enemy_shooter")
        {
            TowerManager EmitterManager1 = attack1.GetComponent<TowerManager>();
            TowerManager EmitterManager2 = attack2.GetComponent<TowerManager>();
            EmitterManager1.defaultZ = transform.eulerAngles.z;
            EmitterManager2.defaultZ = transform.eulerAngles.z;

        }
        else if (enemy.tag == "enemu_spider")
        {
            TowerManager EmitterManager1 = attack1.GetComponent<TowerManager>();
            EmitterManager1.defaultZ = transform.eulerAngles.z;
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

    
}