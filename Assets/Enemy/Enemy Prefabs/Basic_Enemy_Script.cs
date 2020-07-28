using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Basic_Enemy_Script : MonoBehaviour
{
    public float health = 5f;
    public Vector3 birthscale;
    public bool visible = true;
    public bool blinking = false;


    // Start is called before the first frame update
    void Start()
    {
        birthscale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    public void take_damage(float damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (!blinking)
        {
            blinking = true;
            StartCoroutine("blinker");
        }

    }


    IEnumerator blinker()
    {
        for (float i = 1f; i <= 5; i += 1f)
        {
            blink();
            yield return new WaitForSeconds(.15f);
        }
        if (!visible)
        {
            blink();
        }
    }



    void blink()
    {
        if (visible)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            visible = false;
        }
        else
        {
            gameObject.transform.localScale = birthscale;
            visible = true;
        }
        

    }
}
