using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;
    public float time_to_destroy = 5f;
    public float damage = 1f;
    public knight_script knight_s;
    public player_movement player_s;
    public GameObject the_knight;
    public GameObject the_player;
    private string hit_name;


    private void OnEnable()
    {
        Invoke("Destroy", time_to_destroy);
    }
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 5f;
        //Get knight and player script reference.
        the_knight = GameObject.Find("Knight");
        knight_s = (knight_script)the_knight.GetComponent(typeof(knight_script));
        the_player = GameObject.Find("Player");
        player_s = (player_movement)the_player.GetComponent(typeof(player_movement));


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit_name = collision.gameObject.name;
        switch (hit_name)
        {
            case "Knight":
                knight_s.take_damage(damage);
                Destroy();
                break;
            case "Player":
                player_s.take_damage(damage);
                Destroy();
                break;
            case "Block":

                Destroy();
                break;
            default:
                break;



        }
    }


}
