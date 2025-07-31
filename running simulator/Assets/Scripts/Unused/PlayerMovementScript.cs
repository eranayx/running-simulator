using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private Floor floor;
    [SerializeField]
    private float yawSensitivity = 2f;

    private float yaw = 0;

    void FixedUpdate()
    {
        Vector2 moveDirection = gameInput.GetVector2Normalized();

        transform.position += speed * Time.fixedDeltaTime * new Vector3(moveDirection.x, 0, moveDirection.y);

        if (gameInput.Jumped() && floor.IsPlayerGrounded)
        {
            float jumpForce = 7f;
            
            gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        yaw += yawSensitivity * Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(transform.eulerAngles.x, yaw, 0f));
    }
}
