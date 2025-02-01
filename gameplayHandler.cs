using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class gameplayHandler : MonoBehaviour
{
    public Transform monkeyPos;   
    public GameObject background;

    private float segmentHeight;
    private int segmentCount = 0;


    void Start()
    {
        segmentHeight = background.GetComponentsInChildren<SpriteRenderer>()[0].bounds.size.y*2;
        
    }

    void Update()
    {
        if (monkeyPos.position.y > segmentCount*segmentHeight)
        {
            segmentCount++;
            GameObject newSegment = Instantiate(background);

            newSegment.transform.position = new UnityEngine.Vector2(-1.261898f, background.transform.position.y+segmentCount*segmentHeight);
        }
        
    }
}
