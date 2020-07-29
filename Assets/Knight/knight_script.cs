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
    public float damage = 2f;
    public Rigidbody2D rb;
    Vector2 target_transform;
    Vector2 travel_transform;
    public bool knight_movable;
    public bool can_strike;
    public float attack_radius;
    public Basic_Enemy_Script benemy_s;
    public bool colliding_with_enemy = false;
    public float turnSpeed = 1f;
    public Transform attack_point;
    public float attack_cooldown=0;
    public float default_cooldown = 3f;
    public LayerMask enemy_layer;
    


    void Start()
    {
        InvokeRepeating("knight_move_switch", 0f,step);
    }
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemy_list = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject target_enemy = find_closest_enemy(enemy_list);
        if (target_enemy == null)
        {
            //set target enemy equal to door.
        }
        target_transform = target_enemy.transform.position;
        travel_transform = target_transform - rb.position;
        travel_transform.Normalize();
        if (knight_movable)
        {
            move_knight();
        }
        check_in_front();
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
        float angle = Mathf.Atan2(travel_transform.y, travel_transform.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void take_damage(float damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            //knight_dead
        }
    }

  
    private void check_in_front()
    {
        Collider2D[] enemies_hit = Physics2D.OverlapCircleAll(attack_point.position, attack_radius, enemy_layer);
        // implement this for wall detection--Collider2D[] obstacle_hit = Physics2D.OverlapCircleAll(rb.transform.position, attack_radius, 8);
        foreach (Collider2D enemy in enemies_hit)
        {
            try_attack(enemy.gameObject);
        }
    }

    private void try_attack(GameObject enemy)
    {
        if (attack_cooldown <= 0)
        {
            attack(enemy);
        }
    }
    private void attack(GameObject enemy)
    {
        //do attack animation
        Basic_Enemy_Script enemys_s = enemy.GetComponent(typeof(Basic_Enemy_Script)) as Basic_Enemy_Script;
        enemys_s.take_damage(damage);
        StartCoroutine("start_attack_cooldown");
    }

    IEnumerator start_attack_cooldown()
    {
        Debug.Log("I stated cooldown");
        attack_cooldown = default_cooldown;
        for (float i = default_cooldown; i>=1;i-=1f)
        {
            attack_cooldown -= 1f;
            yield return new WaitForSeconds(.5f);
        }
    }

    private GameObject find_closest_enemy(GameObject[] enemy_list)
    {
        GameObject closest_enemy = null;
        if (enemy_list.Length > 0){
            float closest_enemy_dist = 100000;
            Vector2 knight_position = gameObject.transform.position;
            foreach (GameObject enemy in enemy_list)
            {
                if (Vector3.Distance(knight_position, enemy.transform.position) < closest_enemy_dist)
                {
                    closest_enemy = enemy;
                }
            }
        }
        return closest_enemy;
    }
}
