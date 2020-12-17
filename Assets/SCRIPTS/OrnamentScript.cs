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
    public GameObject explosionSprite;
    Vector3 originOfExplode;
    float radius = 3.5f;
    float forceMultiplier = 40;
    public LayerMask objLayer;
    private void OnParticleCollision(GameObject other)
    {
        sr.color = Color.red;
        bombStart = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            sr.color = Color.red;
            bombStart = true;
        }

        sr.color = Color.red;
        bombStart = true;
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
            Instantiate(explosionSprite, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Explosion()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, objLayer);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                //print(col.gameObject.name);
                //float distance;
                //distance = Vector2.Distance(col.transform.position, gameObject.transform.position);
                //if (distance > 1)
                //{
                //    Vector2 force = (col.transform.position - gameObject.transform.position) * (forceMultiplier / distance);
                //}
                //else {
                    

                //}
                Vector2 force = (col.transform.position - gameObject.transform.position) * forceMultiplier;
               
                Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
                rb.AddForce(force, ForceMode2D.Impulse);
                rb.AddTorque(200);
            }
        }
    }
}
