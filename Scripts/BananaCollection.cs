using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using TMPro;

public class BananaCollection : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MonkeyController monkeyController = collision.GetComponent<MonkeyController>();
        if (monkeyController != null)
        {
            monkeyController.CollectBanana();
            Destroy(this.gameObject); // Remove banana from scene
        }
    }
    

    
}
