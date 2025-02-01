using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float timer;
    private float startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > startTime + timer)
        {
            Destroy(gameObject);
        }
    }
}
