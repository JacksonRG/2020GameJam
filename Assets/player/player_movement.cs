﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxHealth = 3f;
    public float health = 3f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    public HealthBar healthBar;

    //public HealthBarPlayer hbPlayer;
    //Dictionary<string, Func> spellEffects;

    public class Spell
    {
        int ID;
        string name;
        string msg; //TODO: Display this on cast (if we choose)
    };

    List<Spell> heldSpells;

    // Update is called once per frame
    void Update()
    {//input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
		//Testing that the healthbar updates
        /*if (Input.GetKeyUp("k")){
			health -= .2f;
			healthBar.SetHealth(health/maxHealth);
        }*/
    }

    void FixedUpdate()
    {//movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    public void take_damage(float damage)
    {
        health = health - damage;
        healthBar.SetHealth(health / maxHealth);
        if (health <= 0)
        {
            //player dead
        }
    }
}
