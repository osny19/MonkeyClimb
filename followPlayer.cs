using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform monkey; // Assign the player in the inspector
    public float fixedX;     // Set this to the desired X position

    void Start()
    {
        if (monkey != null)
        {
            fixedX = transform.position.x; // Lock the initial X position
        }
    }

    void LateUpdate()
    {
        if (monkey != null)
        {
            transform.position = new Vector3(fixedX, monkey.position.y, transform.position.z);
        }
    }
}
