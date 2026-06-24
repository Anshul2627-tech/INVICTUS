using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float runSpeed = 7f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 2f;
    public Transform cameraTarget;

    private Rigidbody rb;
    private float verticalRotation = 0f;
    private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMouseLook();
        HandleRunning();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

        animator.SetBool("isRunning", isMoving);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float speed = isRunning ? runSpeed : moveSpeed;

        Vector3 move = transform.right * h + transform.forward * v;

        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);

        if (cameraTarget != null)
            cameraTarget.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleRunning()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
    }
}