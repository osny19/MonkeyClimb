using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Collections.Generic;


public class gameplayHandler : MonoBehaviour
{
    public Transform monkeyPos;   
    public GameObject background;
    public GameObject background1;
    public List<GameObject> pool = new List<GameObject>(); 
    private float segmentHeight;
    private int segmentCount = 0;


    void Start()
    {
        segmentHeight = background.GetComponentsInChildren<SpriteRenderer>()[0].bounds.size.y*2;
        pool.Add(background);
        pool.Add(background1);
    }

    void Update()
    {
        if (monkeyPos.position.y > segmentCount*segmentHeight)
        {
            segmentCount++;
            int index = UnityEngine.Random.Range(0, pool.Count);
            GameObject chosen = pool[index];

            GameObject newSegment = Instantiate(chosen);

            newSegment.transform.position = new UnityEngine.Vector2(-1.261898f, background.transform.position.y+segmentCount*segmentHeight-0.01f);
        }
        
    }
}
