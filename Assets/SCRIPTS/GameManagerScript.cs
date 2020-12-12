using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] objectsToPlace;
    Queue<GameObject> objectQueue;
    public GameObject SnowCloud;

    // Start is called before the first frame update
    void Start()
    {
        objectQueue = new Queue<GameObject>();
        foreach(GameObject obj in objectsToPlace)
        {
            objectQueue.Enqueue(obj);
        }
        SnowCloud.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject nextObject = Instantiate<GameObject>(objectQueue.Dequeue());
            Vector3 placedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placedPosition.z = 0;
            nextObject.transform.position = placedPosition;
            nextObject.transform.rotation = Quaternion.identity;
            if(objectQueue.Count <= 0)
            {
                StartCloud();
            }
        }
    }

    void StartCloud()
    {
        Debug.Log("Clouds a movin!");
        SnowCloud.SetActive(true);
    }
}
