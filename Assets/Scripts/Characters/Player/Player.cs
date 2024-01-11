using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    public Vector3 groundCheckSize = new Vector3(0.5f, 0.1f, 0.5f);
    public float jumpDelay = 0.2f; // Затримка стрибка
    public float pickupRange = 5.0f; // Дальність, на якій можна підібрати зброю

    private Vector3 velocity;
    private CharacterController controller;
    private float lastJumpTime; // Час останнього стрибка

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        CheckJump();

        Gravity();

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryPickupWeapon();
        }
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        controller.Move(movement * (speed * Time.deltaTime));
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && Time.time - lastJumpTime >= jumpDelay)
        {
            Jump();
        }
    }

    void Jump()
    {
        velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        lastJumpTime = Time.time; // Оновлення часу останнього стрибка
    }

    bool IsGrounded()
    {
        Vector3 cubeCenter = new Vector3(transform.position.x, transform.position.y - controller.height / 2, transform.position.z);
        Collider[] colliders = Physics.OverlapBox(cubeCenter, groundCheckSize, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
    void OnDrawGizmos()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward * pickupRange; // Використовуємо pickupRange для визначення довжини лінії

        Gizmos.color = Color.red; // Виберіть колір для Raycast лінії
        Gizmos.DrawRay(rayOrigin, rayDirection);
    }
    void TryPickupWeapon()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, pickupRange))
        {
            Debug.Log("Picked up: ");
            if (hit.collider.gameObject.CompareTag("Weapon"))
            {
                Debug.Log("Picked up: " + hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject); // Видалення зброї
            }
        }
    }
}
