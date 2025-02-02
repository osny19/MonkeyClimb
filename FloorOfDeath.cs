using UnityEngine;

public class FloorOfDeath : MonoBehaviour
{
    public float speed = 1f; // Starting speed
    public float speedIncreaseRate = 0.05f; // Speed increase per second
    public Transform player; // Assign the player in the inspector
    public float musicTriggerDistance = 15f; // Distance at which music starts (much closer now)
    public AudioSource backgroundMusic; // Background music AudioSource
    public AudioSource intenseMusic; // Intense music AudioSource

    private float initialBackgroundVolume = 0.6f; // Store the initial volume for background music
    private float initialIntenseVolume = 1f; // Store the initial volume for intense music

    void Start()
    {
        // Start both music tracks
        intenseMusic.volume = 0;
        backgroundMusic.Play();
        intenseMusic.Play();
    }

    void Update()
    {
        // Move the floor upwards
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Gradually increase speed over time
        speed += speedIncreaseRate * Time.deltaTime;

        // Calculate the distance to the player
        float distance = Vector3.Distance(transform.position, player.position);

        // Calculate volume factor (1 when close, 0 when far)
        // The closer you get to the player, the more intense music fades in
        float volumeFactor = Mathf.Clamp01(1 - (distance / musicTriggerDistance));

        // Fade out background music and fade in intense music based on the distance
        backgroundMusic.volume = Mathf.Lerp(initialBackgroundVolume, 0, volumeFactor);  // Decrease background music as we approach the player
        intenseMusic.volume = Mathf.Lerp(0, initialIntenseVolume, volumeFactor);  // Increase intense music as we get closer
    }
}
