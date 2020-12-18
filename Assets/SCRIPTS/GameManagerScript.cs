using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] objectsToPlace;
    Queue<GameObject> objectQueue;
    public GameObject SnowCloud;
    public static bool objectsPlaced = false;
    SnowballObject[] snowballs;
    List<GameObject> visualQueue;
    bool checkingWin = false;
    public static int snowballsConnected;
    GameObject ghostObj;
    int obj2Spawn;
    // Start is called before the first frame update
    void Start()
    {
        snowballsConnected = 0;
        if(objectsToPlace.Length == 0) { Debug.LogError("No objects set for placement, attach some to the GM object!!!!!!"); }   
        else {
            objectQueue = new Queue<GameObject>();
            visualQueue = new List<GameObject>();
            GameObject itemSprite = Resources.Load<GameObject>("ItemSprite");
            foreach(GameObject obj in objectsToPlace)
            {
                objectQueue.Enqueue(obj);
                GameObject image = Instantiate<GameObject>(itemSprite);
                image.GetComponent<SpriteRenderer>().sprite = obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                image.transform.localScale = new Vector3(.4f, .4f, .4f);
                visualQueue.Add(image);
            }
            UpdateVisualQueue();
            SnowCloud.SetActive(false);
        }

        SpawnGhostObj();

    }

    // Update is called once per frame
    void Update()
    {
        //Quick Reset Scene
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetMouseButtonDown(0) && ObjectScript.canBePlaced)
        {
            GameObject nextObject = Instantiate<GameObject>(objectQueue.Dequeue());
            Vector3 placedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            placedPosition.z = 0;
            //if (placedPosition.y < 7) {
            //    placedPosition.y = 7;
            //}
            nextObject.transform.position = placedPosition;
            nextObject.transform.rotation = Quaternion.identity;
            nextObject = visualQueue[0];
            visualQueue.RemoveAt(0);
            Destroy(nextObject);
            UpdateVisualQueue();

            obj2Spawn += 1;
            Destroy(ghostObj);
            if (obj2Spawn < objectsToPlace.Length) {
                SpawnGhostObj();
            }

            if (objectQueue.Count <= 0)
            {
                StartCloud();
            }
        }

        if(objectsPlaced) {

            if (CheckWinState())
            {
                StartCoroutine(WinLevel());
            } else {
                Debug.Log("Somethings' not connected");
            }
        }
    }

    private void LateUpdate()
    {
        if (obj2Spawn < objectsToPlace.Length)
        {
            Vector3 ghostPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ghostPos.z = -1;
            ghostObj.transform.position = ghostPos;
        }
       
    }

    void SpawnGhostObj() {
        ghostObj = Instantiate<GameObject>(objectsToPlace[obj2Spawn]);
        ghostObj.transform.GetChild(0).GetComponent<ObjectScript>().ghostObj = true;
        ghostObj.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 0;
        ghostObj.transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale = 0;
        ghostObj.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
       // ghostObj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.45f);
        ghostObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "FG";
        ghostObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 9999;

    }


    void StartCloud()
    {
        SnowCloud.SetActive(true);
       // objectsPlaced = true;
        GameObject[] snowballObjs = GameObject.FindGameObjectsWithTag("snowball");
        snowballs = new SnowballObject[snowballObjs.Length];
        for(int i = 0; i < snowballObjs.Length; i++) {
            snowballs[i] = snowballObjs[i].GetComponent<SnowballObject>();
        }
    }

    bool CheckWinState()
    {
        if (checkingWin) return false;
        foreach(SnowballObject snowball in snowballs)
        {
            if(!snowball.connected)
            {
                return false;
            }
        }
        return true;
    }

    void UpdateVisualQueue()
    {
        float xOffset = 30;
        for(int i = 0; i < visualQueue.Count; i++) {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(20 + (xOffset * i), Screen.height - 30, 0));
            screenPos.z = 0;
            visualQueue[i].transform.position = screenPos;
        }

    }

    IEnumerator WinLevel()
    {
        Debug.Log("won the level!");
        yield return new WaitForSeconds(0.5f);
        checkingWin = false;
        if(CheckWinState()) {
            checkingWin = true;
            float zoomTime = 0.4f;
            float startingZoom = Camera.main.orthographicSize;
            float targetZoom = 4.5f;
            Vector3 cameraStartPos = Camera.main.transform.position;
            Vector3 targetPos = GameObject.FindGameObjectsWithTag("snowball")[0].transform.position;
            targetPos.z = cameraStartPos.z;
            for (float t = 0; t < zoomTime; t += Time.deltaTime)
            {
                Camera.main.transform.position = Vector3.Lerp(cameraStartPos, targetPos, t / zoomTime);
                Camera.main.orthographicSize = Mathf.Lerp(startingZoom, targetZoom, t / zoomTime);
                yield return null;
            }
        }
    }
}
