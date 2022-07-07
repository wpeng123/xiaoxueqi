using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class gamemanager : MonoBehaviour
{
    [HideInInspector] public static gamemanager instance;
    public Gamestate state;
    public float enemys_delay; //秒
    public float wave_max_interval; //秒
    public static event Action<Gamestate> OnGameStateChanged;
    public static int stage=1;//第几关
    public GameObject[] enemys; 
    public GameObject[] Generating_point;
    public String[] Generating_point_check;
    public int[] stage1_wave1;
    public int[] stage1_wave2;
    public int[] stage1_wave3;
    public int[] stage1_wave4;
    public int[] stage1_wave5;
    public int[] stage1_wave6;
    public int[] stage2_wave1;
    public int[] stage2_wave2;
    public int[] stage2_wave3;
    public int[] stage2_wave4;
    public int[] stage2_wave5;
    public int[] stage2_wave6;
    public int[,,] stage_wave;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        stage_wave = new int[2, 6, 5];
        for (int i = 0; i < 5; i++)
        {
            stage_wave[0, 0, i] = stage1_wave1[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[0, 1, i] = stage1_wave2[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[0, 2, i] = stage1_wave3[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[0, 3, i] = stage1_wave4[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[0, 4, i] = stage1_wave5[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[0, 5, i] = stage1_wave6[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[1, 0, i] = stage2_wave1[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[1, 1, i] = stage2_wave2[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[1, 2, i] = stage2_wave3[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[1, 3, i] = stage2_wave4[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[1, 4, i] = stage2_wave5[i];
        }
        for (int i = 0; i < 5; i++)
        {
            stage_wave[1, 5, i] = stage2_wave6[i];
        }
        UpdateGameState(Gamestate.Playing);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateGameState(Gamestate newState)
    {
        state = newState;
        switch (newState)
        {
            case Gamestate.Teaching:
                HandleTeaching();
                break;
            case Gamestate.Playing:
                HandlePlaying();
                break;
            case Gamestate.Prize_Clawing:
                HandlePrize_Clawing();
                break;
            case Gamestate.Updating:
                HandleUpdating();
                break;
            case Gamestate.Defeat:
                HandleDefeat();
                break;
            case Gamestate.Victory:
                HandleVictory();
                break;
        }
OnGameStateChanged?.Invoke(newState);
    }

    private void HandleVictory()
    {
        throw new NotImplementedException();
    }

    private void HandleDefeat()
    {
        throw new NotImplementedException();
    }

    private void HandleUpdating()
    {
        throw new NotImplementedException();
    }

    private async void HandlePrize_Clawing()
    {
        await Task.Delay(10000);
        Debug.Log("stop");
        Time.timeScale = 0;
        //await 抓娃娃机显示


    }

    private async void HandlePlaying()
    {
        for(int j=0;j<6;j++)
        {
            int count=0;
            for(int a=0;a<5;a++)
            {
                count += stage_wave[stage-1, j, a];
            }
            int[] enemy_count = new int[count];
            int index = 0;
            for(int a=0;a<5;a++)
            {
                for(int b=0;b< stage_wave[stage-1, j, a];b++)
                {
                    enemy_count[index] = a;
                    index++;
                }                    
            }
            System.Random r = new System.Random();
            for (int i=0;i<1000;i++)
            {
                int a = r.Next(0, count - 1);
                swap(enemy_count, 0, a);
            }
            while(true)
            {
                GameObject k;
                if(j<5)
                k = Generating_point[r.Next(0, Generating_point.Length)];
                else
                k = Generating_point[0];
                if(Generating_point_check[Array.IndexOf(Generating_point,k)][(stage - 1) * 6 + j]=='1')
                {
                    Instantiate(enemys[enemy_count[count - 1]], k.transform);
                    count--;
                    if (count == 0)
                    {
                        Debug.Log("end");
                        break;
                    }
                    await Task.Delay((int)(enemys_delay * 1000));
                }              
            }
            for(int i=0;i< wave_max_interval;i++)
            {
                //检测敌人是否被消灭完
                /*if(over)
                {
                    break;
                }*/
                await Task.Delay((int)(1000));
            }     
        }
        UpdateGameState(Gamestate.Prize_Clawing);
        return;
    }

    private void HandleTeaching()
    {
        //教学，下一个状态为
        UpdateGameState(Gamestate.Playing);
    }

    public enum Gamestate
    {
        Teaching,
        Playing,
        Prize_Clawing,
        Updating,
        Defeat,
        Victory
    }
    private void swap(int[] temp,int a,int b)
    {
        int t;
        t = temp[a];
        temp[a] = temp[b];
        temp[b] = t;
    }
}
//this.GetType().GetField(str).GetValue(this).ToString()