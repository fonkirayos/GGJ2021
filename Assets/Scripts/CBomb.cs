﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBomb : MonoBehaviour
{
    public GameObject bomb;
    float timeToExplode = 2f;
    float radius = 1f;
    float timeElapsed = 0f;
    float lifespan = 3f;
    public bool bexplode = false;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), GameObject.Find("Player").GetComponent<CapsuleCollider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(timeElapsed >= timeToExplode)
        {
            if (!bexplode)
            {
                Debug.Log("explode");
                explode();
            }
        }
        if(timeElapsed >= lifespan)
        {
            Destroy(bomb);
        }
        timeElapsed += Time.deltaTime;
    }

    void explode()
    {
        bexplode = true;
       Collider2D[]  collisions = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D col in collisions)
        {
            if(col.gameObject.tag == "wall")
            {
                Destroy(col.gameObject);
            }
            else if(col.gameObject.tag == "Player")
            {
                Debug.Log("jeje explotamos alv");
                col.gameObject.GetComponent<CPlayerController>().recoverJumps();
            }
        }
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(transform.position, radius);
    //}
}
