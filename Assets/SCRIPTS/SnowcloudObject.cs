using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowcloudObject : MonoBehaviour
{

    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "check4win")
        {
            print("Cloud has finished its course!");
            GameManagerScript.objectsPlaced = true;
        }
    }
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime);
    }
}
