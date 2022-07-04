using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
        Debug.Log("1");
        playermove2 ifplayer = other.collider.GetComponent<playermove2>();
        if (ifplayer != null)
        {
            Debug.Log("2");
            Death();
        }
    }

    void Death() //死亡（播放动画以及死亡）
    {
        Destroy(this.transform.parent.gameObject);
    }

}
