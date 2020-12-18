using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    SpriteRenderer sr;
    public static bool canBePlaced;
    public bool ghostObj;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (ghostObj) {
            canBePlaced = false;
            sr.color = new Color(255, 0, 0, 0.35f);

        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (ghostObj)
        {
            canBePlaced = true;
            sr.color = new Color(37, 255, 0, 0.35f);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        if (ghostObj)
        {
            canBePlaced = true;
            sr.color = new Color(37, 255, 0, 0.35f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
