﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowcloudObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector2.right * Time.deltaTime * 1f);
    }
}
