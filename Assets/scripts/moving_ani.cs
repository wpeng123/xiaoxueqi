using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_ani : MonoBehaviour
{
    Animator animator;
    public float anispeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.GetComponentInParent<Rigidbody2D>().velocity.Equals(Vector2.zero))
        {
            animator.SetBool("moving", true);
            animator.SetFloat("speed", this.GetComponentInParent<playermove2>().getspeed()*anispeed);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
}
