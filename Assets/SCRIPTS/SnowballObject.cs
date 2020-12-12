using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballObject : MonoBehaviour
{
    Rigidbody2D rb;
    public List<GameObject> snowHitAlready = new List<GameObject>();

    private void OnParticleCollision(GameObject other)
    {
        var magnitude = 600;
        var force = transform.position - other.transform.position;
        force.Normalize();
        rb.AddForce(force * magnitude);
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
