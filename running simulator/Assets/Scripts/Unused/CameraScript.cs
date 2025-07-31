using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private GameInput gameInput;
    [SerializeField]
    private float pitchSensitivity = 2f;


    private float pitch = 0;

    private void FixedUpdate()
    {
        float heightOffset = 0.5f;
        transform.position = objectToFollow.position + Vector3.up * heightOffset;

        pitch -= pitchSensitivity * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0f);
    }
}
