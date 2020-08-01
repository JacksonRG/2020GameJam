using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dark_Mage_Script : MonoBehaviour
{
    Vector2 movement;
    public Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
        movement.x = Input.GetAxisRaw("Horizontal")*-1;
        movement.y = Input.GetAxisRaw("Vertical")*-1;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
