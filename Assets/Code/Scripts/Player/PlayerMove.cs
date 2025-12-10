using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // Adjust speed in the Inspector

    void Update()
    {
        // Get input from keyboard axes (WASD/Arrow Keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // Move the player
        // Time.deltaTime ensures movement is smooth regardless of frame rate
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
