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
        var magnitude = 100;
        var force = transform.position - other.transform.position;
        force.Normalize();
        rb.AddForce(force * magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "accessory")
        {
            collision.gameObject.transform.parent = transform;
            collision.gameObject.transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "snowball")
        {
            if (!connected)
            {
                connected = true;
                GameManagerScript.snowballsConnected += 1;
            }
           
           
            collision.gameObject.GetComponent<SnowballObject>().connected = true;
            Debug.Log("Connected");


            ///Snowballs will not roll with the hinge joint
            //HingeJoint2D myJoint = (HingeJoint2D)gameObject.AddComponent<HingeJoint2D>();
            //myJoint.connectedBody = collision.rigidbody;

            ///Snowballs will roll together with the distance joint
            DistanceJoint2D myJoint = (DistanceJoint2D)gameObject.AddComponent<DistanceJoint2D>();
            myJoint.connectedBody = collision.rigidbody;
            


        }
       
    }

    ///Removed this as we dont have a use or mechanic for breaking a connection once they are connected at this point and seemed to cause issues in win state with joints?
    ///
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "snowball")
    //    {
    //        connected = false;
    //        Debug.Log("Disconnected");
    //    }
    //}
}
