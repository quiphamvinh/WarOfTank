using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    private float moveSpeed;
    private bool moveRight = true;

    private void Start()
    {
        moveSpeed = 2f;
    }

    private void Update()
    {
        if (transform.position.x > -1f)
        {
            moveRight = false;
        }
        else if (transform.position.x < -15f)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }
    }
}