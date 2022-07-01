using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Ai_needed_variable
{//Ai�ű���Ҫ��������ֵ����
    public float target_position_x;//��ǰtarget����λ��
    public float target_position_y;
    public float target_position_z;

    public float enemy_position_x;//��ǰenemy����λ��
    public float enemy_position_y;
    public float enemy_position_z;

    public float weight_stage_03;
    public float weight_stage_05;//Ȩ��
    public float weight_stage_10;
    public float weight_stage_20;
    public float weight_stage_40;

    public float nearest_collider_x;//�����ײ���λ��
    public float nearest_collider_y;

    public float distance;
    public float target_distance;

    public float enemy_type3_distance;

    public float enemy_type2_nearest;
    public float enemy_type2_farthest;

    // float distance_x;//ai��Ŀ��֮��ľ��� ֱ��
    //float distance_y;
    //float distance_z;
    public float distance_apart;
    //float distance_apart_x;//ai��Ŀ��֮����Ҫ���ֵľ���
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
    public Rigidbody2D enemy;//��ȡ�����Լ�����Ŀ��
    public Rigidbody2D target;

    public GameObject position_0;
    public GameObject position_1;
    public GameObject position_2;
    public GameObject position_3;

    public GameObject main_obj;

    public Situation situation;
    public int counter;//���ڼ�¼�����˶���� ����סtime
    void initialie()
    {
        if (this.tag == "enemy_type1")
            position_stru.distance_apart = 1f;//enemy1��Ҫ��target֮�䱣�ֵľ���

        else if (this.tag == "enemy_type2")
        {

            position_stru.enemy_type2_nearest = 3;
            position_stru.enemy_type2_farthest = 4;
            position_stru.distance_apart = position_stru.enemy_type2_farthest;//enemy2��Ҫ��target֮�䱣�ֵľ���
        }
        else if (this.tag == "enemy_type3")
        {
            position_stru.distance_apart = 0.5f;//enemy3��Ҫ��target֮�䱣�ֵľ���
            position_stru.enemy_type3_distance = 3;//enemy3 �����Ƕ���ʱ�л�Ŀ��Ϊ���
        }
        else
        {
            position_stru.distance_apart = 1f;//enemy2��Ҫ��target֮�䱣�ֵľ���
        }



        position_stru.weight_stage_03 = 10.70f;
        position_stru.weight_stage_05 = 8.50f;//��ͬ�����ƶ��ٶȵ�Ȩֵ
        position_stru.weight_stage_10 = 8.00f;
        position_stru.weight_stage_20 = 7.00f;
        position_stru.weight_stage_40 = 0.10f;

        situation = Situation.normal;//��ǰ�����

    }
    void get_gameobj()
    {


        int num = Random.Range(0, 5);

        // Debug.Log("��������"+num);
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
    Vector2 getforce()//�õ���ǰ���ķ����Լ�Ȩֵ
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
            //;InvokeRepeating("get_gameobj", 2, 1000);  //2���û0.3f����һ�� 

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
    float get_distance()//�õ�enemy�Լ�target�ľ���
    {
        float enemy_distance_x = enemy.transform.position.x;
        float target_distance_x = target.transform.position.x;
        //Debug.Log(enemy_distance_x+ "  " +target_distance_x);
        float distance_sqre = Mathf.Pow(enemy.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(enemy.transform.position.y - target.transform.position.y, 2);

        return Mathf.Sqrt(distance_sqre);
    }
    float get_distance(GameObject target)//�õ�enemy�Լ�target�ľ���
    {
        float enemy_distance_x = enemy.transform.position.x;
        float target_distance_x = target.transform.position.x;
        //Debug.Log(enemy_distance_x+ "  " +target_distance_x);
        float distance_sqre = Mathf.Pow(enemy.transform.position.x - target.transform.position.x, 2) + Mathf.Pow(enemy.transform.position.y - target.transform.position.y, 2);

        return Mathf.Sqrt(distance_sqre);
    }
    void update_information()//��ʼ����������
    {
        position_stru.target_distance = get_distance();

        if (this.tag == "enemy_type3" && position_stru.target_distance > position_stru.enemy_type3_distance)
        {
            //;InvokeRepeating("get_gameobj", 2, 1000);  //2���û0.3f����һ�� 

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
            InvokeRepeating("get_gameobj", 2, 5f);  //2���û0.3f����һ�� 

        }
        update_information();
        //����enemy1 3
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
        //����enemy2
        if (this.tag == "enemy_type2" && get_distance() < position_stru.enemy_type2_nearest)
        {
            enemy.velocity = getforce();
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");//W��S/ǰ�������
        //��ȡ����float��ʼ��0�����а�������ʱ�������/�ݼ���ȡֵ��Χ��-1��1��������ģ��һ��������ٵĹ���
        target.AddForce(new Vector3(h, v, 0));//vector3��ʸ����ģ����������ϵ���




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
