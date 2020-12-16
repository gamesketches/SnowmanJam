using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfScript : MonoBehaviour
{
    float timeTillDestroy = .25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTillDestroy -= Time.deltaTime;
        if (timeTillDestroy <= 0) {
            Destroy(gameObject);
        }
    }
}
