using UnityEngine;

public class KillZone : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MonkeyController monkeyController = collision.GetComponent<MonkeyController>();
        if(monkeyController != null)
        {
            monkeyController.Die();
        }
    }
}
