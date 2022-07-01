using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Ai_needed_variable{//Ai脚本需要的两个数值变量
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
    public float last_distance;
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
    normal,lock_x_l,lock_x_r,lock_y_u,lock_y_d
}

public class Ai_Move_main : MonoBehaviour
{
    // Start is called before the first frame update
    public Ai_needed_variable position_stru;
    public Rigidbody2D enemy;//获取敌人以及敌人目标
    public Rigidbody2D target;
    public Situation situation;
    public int counter;//用于记录球体运动情况 被卡住time
    void initialie()
    {
        position_stru.distance_apart = 0.1f;//enemy需要与target之间保持的距离

        position_stru.weight_stage_03 = 10.70f;
        position_stru.weight_stage_05 = 8.50f;//不同距离移动速度的权值
        position_stru.weight_stage_10 = 8.00f;
        position_stru.weight_stage_20 = 7.00f;
        position_stru.weight_stage_40 = 0.10f;

        situation = Situation.normal;//当前的情况

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

        Debug.Log(direction);
        return direction;
    }
    float get_distance()//得到enemy以及target的距离
    {
        float enemy_distance_x = enemy.transform.position.x;
        float target_distance_x = target.transform.position.x;
        //Debug.Log(enemy_distance_x+ "  " +target_distance_x);
        float distance_sqre = Mathf.Pow(enemy.transform.position.x - target.transform.position.x,2)+ Mathf.Pow(enemy.transform.position.y - target.transform.position.y, 2);
        
        return Mathf.Sqrt(distance_sqre);
    }
    
    void update_information()
    {
        position_stru.target_position_x = target.transform.position.x;
        position_stru.target_position_y = target.transform.position.y;
        position_stru.target_position_z = target.transform.position.z;

        position_stru.enemy_position_x = enemy.transform.position.x;
        position_stru.enemy_position_y = enemy.transform.position.y;
        position_stru.enemy_position_z = enemy.transform.position.z;

        position_stru.last_distance = position_stru.distance;
        position_stru.distance = get_distance();

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
    void Update()
    {
        update_information();
        if (position_stru.distance > position_stru.distance_apart)
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
