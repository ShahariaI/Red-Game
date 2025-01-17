using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pandemoniumai : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    SpriteRenderer sr;

    private bool isFacingRight = true;
    private float horizontal;

    private float distance;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
        

            if (distance < distanceBetween)
            {
                Vector2 direction = player.transform.position - transform.position;
                direction.Normalize();

                if ( direction.x < 0)
                {
                    sr.flipX = false;
                }
                if (direction.x > 0)
                {
                    sr.flipX = true;
                }

                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.transform.position,
                    speed * Time.deltaTime
                );
            }
        }
    }
   
}
