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

    Vector3 originOfExplode;
    float radius = 3.5f;
    float forceMultiplier = 30; 

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
        sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

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
            Destroy(gameObject);
        }
    }

    public void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                //print(col.gameObject.name);
                Vector2 force = (col.transform.position - gameObject.transform.position) * (forceMultiplier / Vector2.Distance(col.transform.position, gameObject.transform.position));
                Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}
