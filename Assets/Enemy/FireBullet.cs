using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount;

    [SerializeField]
    private float startAngle, endAngle;

    [SerializeField]
    private float fire_frequency;

    private Vector2 BulletMoveDirection;

    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 1f, fire_frequency);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount - 1; i++)
        {
            float bulDirX = transform.position.x - Mathf.Sin((angle * Mathf.PI) / 180);
            float bulDirY = transform.position.y - Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.localScale = new Vector3(1, 1, 1);
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(bulDir);

            angle += angleStep;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
