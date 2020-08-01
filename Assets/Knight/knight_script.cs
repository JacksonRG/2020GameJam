using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
using Pathfinding;


public class knight_script : MonoBehaviour
{
    //Attributes
    public float moveSpeed = 200f;
    public float step = 0.1f;//How Often a step occurs
    public float health = 10f;
    public float maxHealth = 10f;
    public float damage = 2f;//How much damage can be dealt with each attack.
    public bool knight_movable;//Can the knight move right now? Switches on and off. Is turned off when attacking.
    public bool can_strike;//Bool to determine if cooling down.
    public float attack_radius;//radius of the attack point object attached to the knight. NOT THE RADIUS AROUND KNIGHT.
    public float turnSpeed = 1f;//How fast the knight reorients himself. 
    public float attack_cooldown=0;
    public float default_cooldown = 3f;
    //Knight Components
    public Rigidbody2D rb;
    public Transform attack_point;
    public HealthBar healthBar;
    public GameObject exit;
    public LayerMask exit_layer;
    public LevelLoader level_loader;
    //Enemy Interaction
    public Basic_Enemy_Script benemy_s;//script of enemy that is being hit. Used to call take damage.
    public LayerMask enemy_layer;//Establishes the enemy layer. Used in striking hitbox to determine if anyone is hit.
    public GameObject[] enemy_list;
    //Pathfinding and Movement
    public AIPath aiPath;
    public float nextWaypointDistance = 3f;
    Vector2 target_transform;
    Vector2 travel_transform;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;//This is reference to knight for the AI Plugin.


    void Start()
    {
        seeker = GetComponent<Seeker>();
        exit = GameObject.FindGameObjectWithTag("Exit");
        InvokeRepeating("knight_move_switch", 0f,step);
    }
    // Update is called once per frame

    void Update()
    {
        enemy_list = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject target_enemy = find_closest_enemy(enemy_list);
        if (target_enemy == null)
        {
            target_enemy = exit;
        }
        seeker.StartPath(rb.position, target_enemy.transform.position, OnPathComplete);
        //if ((currentWaypoint >= path.vectorPath.Count) && (target_enemy == exit))
        //{
        //    reachedEndOfPath = true;
        //}
        //else
        //{
        //    reachedEndOfPath = false;
        //}
        if (knight_movable)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            move_knight(direction);
        }
        check_in_front();
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            //currentWaypoint = 0;
        }
        else
        {
            knight_movable = false;
            Debug.Log("Error in path");
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
    void move_knight(Vector2 direction)
    {//movement
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance <= nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    public void take_damage(float damage)
    {
        health = health - damage;
		healthBar.SetHealth(health/maxHealth);
        if (health <= 0)
        {
            //knight_dead
        }
    }
    private void check_in_front()
    {
        Collider2D[] enemies_hit = Physics2D.OverlapCircleAll(attack_point.position, attack_radius, enemy_layer);
        Collider2D exit_hit = Physics2D.OverlapCircle(attack_point.position, attack_radius, exit_layer);
        // implement this for wall detection--Collider2D[] obstacle_hit = Physics2D.OverlapCircleAll(rb.transform.position, attack_radius, 8);
        foreach (Collider2D enemy in enemies_hit)
        {
            knight_movable = false;
            try_attack(enemy.gameObject);
        }
        if((exit_hit != null) && (enemy_list.Length==0))
        {
            Debug.Log("I Am going to transition now.");
            level_loader.LoadNextLevel();
            //Transition to next scene.
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
                    closest_enemy_dist = Vector3.Distance(knight_position, enemy.transform.position);
                    closest_enemy = enemy;
                }
            }
        }
        return closest_enemy;
    }
}
