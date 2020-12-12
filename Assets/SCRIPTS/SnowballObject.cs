using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballObject : MonoBehaviour
{
    Rigidbody2D rb;
    public List<GameObject> snowHitAlready = new List<GameObject>();
    public bool connected;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.tag = "snowball";
        connected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        var magnitude = 600;
        var force = transform.position - other.transform.position;
        force.Normalize();
        rb.AddForce(force * magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "snowball")
        {
            connected = true;
            collision.gameObject.GetComponent<SnowballObject>().connected = true;
            Debug.Log("Connected");
        }
        else if(collision.gameObject.tag == "accessory")
        {
            collision.gameObject.transform.parent = transform;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "snowball")
        {
            connected = false;
            Debug.Log("Disconnected");
        }
    }
}
