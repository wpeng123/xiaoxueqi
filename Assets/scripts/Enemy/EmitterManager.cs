using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterManager : MonoBehaviour
{
    public float time;
    public GameObject shell;
    public float shellspeed;
    public float shellscale;
    public float shellDeathTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn() //Éú³É×Óµ¯
    {
        GameObject go = (GameObject)Instantiate(shell);
        go.transform.localScale = new Vector3(shellscale, shellscale);
        ShellManager b = go.GetComponent<ShellManager>();
        b.speed = shellspeed;
        b.time = shellDeathTime;
        go.transform.localPosition = this.transform.position;
        Invoke("Spawn", time);
    }

}
