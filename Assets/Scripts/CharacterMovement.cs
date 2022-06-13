using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 100f;
    protected Rigidbody rb;
    protected Animator anim;
    public bool canMove = true;
    protected float verDirect = 1;
    protected float sprintValue = 0f;
    public bool isFinish = false;
    public bool IsInvulnerable { get; private set; }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.velocity = Vector3.forward * verDirect * (sprintValue +1) * moveSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    public bool IsMove()
    {
        return rb.velocity.magnitude > 0.1f;
    }
    public virtual void Die()
    {
        anim.SetTrigger("Death");
        canMove = false; 
        
    }
    public virtual void Win()
    {
        IsInvulnerable = true;
        isFinish = true;
        canMove = false;
        
    }
    

}
