using UnityEngine;

public class TankController : MonoBehaviour
{
    public Joystick joystick;

    public float moveSpeed = 3.5f;
    public float turnSpeed = 360f;
    private Vector2 moveDirection;
    Rigidbody2D r2d;
    float effectTime = 0f;
    float maxTime = 3f;
    private bool hasSpeedUp = false;
    float currentRotationZ;
    float targetRotationZ;
    bool isRotating = false;

    AudioManager audioManager;


    void Start()
    {
        currentRotationZ = transform.rotation.eulerAngles.z;
        targetRotationZ = transform.rotation.eulerAngles.z;
    }

    private void LateUpdate()
    {
        if (joystick != null)
        {
            moveDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        }

        if (moveDirection.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            targetRotationZ = angle;
            isRotating = true;
        }

        if (isRotating)
        {
            float step = turnSpeed * Time.deltaTime;
            currentRotationZ = Mathf.MoveTowardsAngle(currentRotationZ, targetRotationZ, step);
            transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);

            if (currentRotationZ == targetRotationZ)
            {
                isRotating = false;
            }
        }
        r2d.velocity = moveDirection * moveSpeed;

        if (hasSpeedUp)
        {
            if (effectTime > 0)
            {
                effectTime -= Time.deltaTime;

                if (effectTime <= 0)
                {
                    effectTime = 0;
                    hasSpeedUp = false;
                    moveSpeed = 3.5f;
                }
            }
        }
    }

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindWithTag("JoystickMove").GetComponent<Joystick>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!hasSpeedUp && col.tag == "SpeedUp")
        {
            if (moveSpeed <= 10f)
            {
                audioManager.PlaySFX(audioManager.addSpeed);
                effectTime = maxTime;
                moveSpeed = moveSpeed * 2;
                Destroy(col.gameObject);
                hasSpeedUp = true;
            }
        }
    }
}