using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrnamentScript : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    public float bombTimer = 3;
    bool bombStart;
    bool hasExploded;

    Vector2 originOfExplode;
    float radius;
    float forceMultiplier; // force of the explosion

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 8)
        {
            sr.color = Color.red;
            bombStart = true;
        }

    }


    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (bombStart)
        {
            bombTimer -= Time.deltaTime;
        }

        if (bombTimer <= 0 && !hasExploded)
        {
            Explosion();
            hasExploded = true;
        }
    }

    public void Explosion()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(originOfExplode, radius);

        foreach (Collider2D col in colliders)
        {
            // the force will be a vector with a direction from origin to collider's position and with a length of 'forceMultiplier'
            Vector2 force = (col.transform.position - originOfExplode) * forceMultiplier;
            Rigidbody rb = col.transform.GetComponent<Rigidbody>();
            rb.AddForce(force);
        }
    }
}
