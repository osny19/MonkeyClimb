using UnityEngine;

public class VineArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        MonkeyController monkeyController = other.GetComponent<MonkeyController>();
        if (monkeyController != null)
        {
            monkeyController.inVine = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MonkeyController monkeyController = other.GetComponent<MonkeyController>();
        if (monkeyController != null)
        {
            monkeyController.inVine = false;
        }
    }

}
