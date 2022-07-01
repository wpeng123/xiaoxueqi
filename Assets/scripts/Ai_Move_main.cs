using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Ai_needed_variable
{//Ai脚本需要的两个数值变量
    public float target_position_x;//当前target所在位置
    public float target_position_y;
    public float target_position_z;

    public float enemy_position_x;//当前enemy所在位置
    public float enemy_position_y;
    public float enemy_position_z;

    public float weight_stage_03;
    public float weight_stage_05;//权重
    public float weight_stage_10;
    public float weight_stage_20;
    public float weight_stage_40;

    public float nearest_collider_x;//最近碰撞体的位置
    public float nearest_collider_y;

    public float distance;
    public float target_distance;

    public float enemy_type3_distance;

    public float enemy_type2_nearest;
    public float enemy_type2_farthest;

    // float distance_x;//ai与目标之间的距离 直线
    //float distance_y;
    //float distance_z;
    public float distance_apart;
    //float distance_apart_x;//ai与目标之间需要保持的距离
    //float distance_apart_y;
    //float distance_apart_z;
    public Vector2 direction;
}
public enum Situation
{
    normal, lock_x_l, lock_x_r, lock_y_u, lock_y_d
}

public class Ai_Move_main : MonoBehaviour
{
    // Start is called before the first frame update
    public Ai_needed_variable position_stru;
    public Rigidbody2D enemy;//获取敌人以及敌人目标
    public Rigidbody2D target;

    public GameObject position_0;
    public GameObject position_1;
    public GameObject position_2;
    public GameObject position_3;

    public GameObject main_obj;

    public Situation situation;
    public int counter;//用于记录球体运动情况 被卡住time
    void initialie()
    {
        if (this.tag == "enemy_type1")
            position_stru.distance_apart = 1f;//enemy1需要与target之间保持的距离

        else if (this.tag == "enemy_type2")
        {

            position_stru.enemy_type2_nearest = 3;
            position_stru.enemy_type2_farthest = 4;
            position_stru.distance_apart = position_stru.enemy_type2_farthest;//enemy2需要与target之间保持的距离
        }
        else if (this.tag == "enemy_type3")
        {
            position_stru.distance_apart = 0.5f;//enemy3需要与target之间保持的距离
            position_stru.enemy_type3_distance = 3;//enemy3 距离是多少时切换目标为玩家
        }
        else
        {
            position_stru.distance_apart = 1f;//enemy2需要与target之间保持的距离
        }



        position_stru.weight_stage_03 = 10.70f;
        position_stru.weight_stage_05 = 8.50f;//不同距离移动速度的权值
        position_stru.weight_stage_10 = 8.00f;
        position_stru.weight_stage_20 = 7.00f;
        position_stru.weight_stage_40 = 0.10f;

        situation = Situation.normal;//当前的情况

    }
    void get_gameobj()
    {


        int num = Random.Range(0, 5);

        // Debug.Log("被调用啦"+num);
        if (num == 0)
        {
            main_obj = position_1;
        }
        else if (num == 1)
        {
            main_obj = position_2;
        }
        else if (num == 2)
        {
            main_obj = position_3;
        }
        else
        {
            main_obj = position_0;
        }
        position_stru.target_position_x = main_obj.transform.position.x;
        position_stru.target_position_y = main_obj.transform.position.y;
        position_stru.target_position_z = main_obj.transform.position.z;

    }
    Vector2 getforce()//得到当前力的方向以及权值
    {
        float weight = 0;
        if (position_stru.distance > 20f)
            weight = position_stru.weight_stage_40;
        else if (position_stru.distance > 10f)
            weight = position_stru.weight_stage_20;
        else if (position_stru.distance > 5f)
            weight = position_stru.weight_stage_10;

        else if (position_stru.distance > 3f)
            weight = position_stru.weight_stage_05;
        else
            weight = position_stru.weight_stage_03;

        if (this.tag == "enemy_type2" && get_distance() < position_stru.enemy_type2_nearest)
        {
            //;InvokeRepeating("get_gameobj", 2, 1000);  //2秒后，没0.3f调用一次 

            weight *= -1;
        }

        float dir_x = (position_stru.target_position_x - position_stru.enemy_position_x);
        int sign_x = dir_x >= 0 ? 1 : -1;
        float dir_y = (position_stru.target_position_y - position_stru.enemy_position_y);
        int sign_y = dir_y >= 0 ? 1 : -1;

        float abs_dir_x = Mathf.Abs(dir_x);
        float abs_dir_y = Mathf.Abs(dir_y);

        float scale_dir_x = (abs_dir_x / (abs_dir_x + abs_dir_y)) * weight * sign_x;
        float scale_dir_y = (abs_dir_y / (abs_dir_x + abs_dir_y)) * weight * sign_y;
        Vector2 direction;
        direction.x = scale_dir_x;
        direction.y = scale_dir_y;

        //Debug.Log(direction);
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
    float get_distance(GameObject target)//得到enemy以及target的距离
    {
        float enemy_distance_x = enemy.transform.position.x;
        float target_distance_x = target.transform.position.x;
        //Debug.Log(enemy_distance_x+ "  " +target_distance_x);
        float distance_sqre = Mathf.Pow(enemy.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(enemy.transform.position.y - target.transform.position.y, 2);

        return Mathf.Sqrt(distance_sqre);
    }
    void update_information()//初始化距离坐标
    {
        position_stru.target_distance = get_distance();

        if (this.tag == "enemy_type3" && position_stru.target_distance > position_stru.enemy_type3_distance)
        {
            //;InvokeRepeating("get_gameobj", 2, 1000);  //2秒后，没0.3f调用一次 

            position_stru.distance = get_distance(main_obj);
        }
        else
        {
            position_stru.target_position_x = target.transform.position.x;
            position_stru.target_position_y = target.transform.position.y;
            position_stru.target_position_z = target.transform.position.z;

            //Debug.Log("change tag");
            position_stru.distance = get_distance();
        }


        position_stru.enemy_position_x = enemy.transform.position.x;
        position_stru.enemy_position_y = enemy.transform.position.y;
        position_stru.enemy_position_z = enemy.transform.position.z;




        //Debug.Log("position_stru.position_x" + position_stru.position_x + " position_stru.position_y" + position_stru.position_y + "position_stru.diatance" + position_stru.distance);
        //Debug.Log("position_stru.diatance" + position_stru.distance);
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
    int judje = 0;
    void Update()
    {

        if (enemy.tag == "enemy_type3" && judje == 0)
        {
            judje = 1;
            InvokeRepeating("get_gameobj", 2, 5f);  //2秒后，没0.3f调用一次 

        }
        update_information();
        //处理enemy1 3
        if (position_stru.distance > position_stru.distance_apart)
        {
            // enemy.AddRelativeForce(getforce());
            /*if (position_stru.last_distance < position_stru.distance + 0.05 && position_stru.last_distance > position_stru.distance - 0.05)
                counter++;
            if (counter >= 4)
            {
                if (situation != Situation.normal)
                {
                    situation = Situation.normal;
                    counter = 0;
                }


                //if()
            }*/
            switch (situation)
            {
                case Situation.normal:
                    enemy.velocity = getforce();
                    break;
                case Situation.lock_x_r:
                case Situation.lock_x_l:
                    {
                        Vector2 dir = getforce();
                        dir.x = 0;
                        enemy.velocity = dir;
                    }
                    break;
                case Situation.lock_y_u:
                case Situation.lock_y_d:
                    {
                        Vector2 dir = getforce();
                        dir.y = 0;
                        enemy.velocity = dir;
                    }
                    break;

            }

            //enemy.AddForce(getforce());
            //print("add force" + getforce());
        }
        else
        {
            enemy.velocity = Vector3.zero;
            //print("stop");
        }
        //处理enemy2
        if (this.tag == "enemy_type2" && get_distance() < position_stru.enemy_type2_nearest)
        {
            enemy.velocity = getforce();
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");//W、S/前、后方向键
        //获取到的float初始是0，在有按键按下时，会递增/递减（取值范围是-1～1），可以模拟一个缓冲加速的过程
        target.AddForce(new Vector3(h, v, 0));//vector3是矢量，模拟的是物理上的力




        //print("horizontal: " + h + ",vertical:" + v);0~1

    }
    private void OnCollisionEnter2D(Collision2D ctl)
    {
        //ContactPoint contact = ctl.contacts[0];
        // Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Debug.Log("position" + pos);
        position_stru.nearest_collider_x = ctl.transform.position.x;
        position_stru.nearest_collider_y = ctl.transform.position.y;
        //print(ctl.gameObject.name + "position is"+ctl.transform.position);
    }
}
