using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;

public class knight_script : MonoBehaviour
{

    public float moveSpeed = .5f;
    public float step = 0.1f;
    public float health = 10f;
    public Rigidbody2D rb;
    Vector2 target_transform;
    Vector2 travel_transform;
    public bool knight_movable;
    public bool can_strike;
    public float striking_distance =5f;
    
    void Start()
    {
        InvokeRepeating("knight_move_switch", 0f,step);
    }
    // Update is called once per frame
    void Update()
    {
        GameObject temp = GameObject.Find("Basic_Enemy");
        target_transform = temp.transform.position;
        travel_transform = target_transform - rb.position;
        travel_transform.Normalize();
        if (knight_movable)
        {
            move_knight();
        }
    }

    void knight_move_switch()
    {
        if (knight_movable)
        {
            knight_movable = false;
        }
        else
        {
            knight_movable = true;
        }
    }


    void move_knight()
    {//movements
        rb.MovePosition(rb.position + travel_transform * moveSpeed * Time.fixedDeltaTime);
        
    }

    public void take_damage(float damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            //knight_dead
        }
    }
}
