using UnityEngine;
using Cinemachine;

public class FirstPersonController : MonoBehaviour
{
    private const float GRAVITY_SCALE = 3f;
    private const float JUMP_HEIGHT = 2f;
    private const float SENSITIVITY = 0.15f;
    private const float PITCH_LIMIT = 50f;

    public Vector2 moveInput;
    public Vector2 lookInput;

    [SerializeField] private CinemachineVirtualCamera firstPersonCamera;
    [SerializeField] private CharacterController characterController;

    private float currentPitch = 0f;
    private float verticalVelocity = 0f;
    public bool IsGrounded => characterController.isGrounded;

    public float CurrentPitch
    {
        get => currentPitch;

        private set
        {
            currentPitch = Mathf.Clamp(value, -PITCH_LIMIT, PITCH_LIMIT);
        }
    }

    private void OnValidate()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        MoveUpdate();
        LookUpdate();
    }

    private void LookUpdate()
    {
        if (!Player.Instance.IsInShop)
        {
            Vector2 rotation = SENSITIVITY * new Vector2(lookInput.x, lookInput.y);

            // Looking up and down
            CurrentPitch -= rotation.y;
            firstPersonCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);

            // Looking left and right
            transform.Rotate(Vector3.up * rotation.x);
        }
    }

    private void MoveUpdate()
    {
        Vector3 motion = transform.right * moveInput.x + transform.forward * moveInput.y;
        motion.y = 0f;
        motion.Normalize();

        
        if (IsGrounded && verticalVelocity <= 0.01f)
        {
            // Ground the player
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * GRAVITY_SCALE * Time.deltaTime;
        }

        Vector3 magnifiedMotion = Player.Instance.Speed * motion;

        characterController.Move(new Vector3(magnifiedMotion.x, verticalVelocity, magnifiedMotion.z) * Time.deltaTime);
    }

    public void TryJump()
    {
        if (IsGrounded)
        {
            verticalVelocity = Mathf.Sqrt(JUMP_HEIGHT * -2f * Physics.gravity.y * GRAVITY_SCALE);
        } 
    }

    public bool IsMoving()
    {
        return characterController.velocity != Vector3.zero;
    }
}
