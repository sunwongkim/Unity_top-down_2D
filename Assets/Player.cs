using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    Animator animator;
    Vector2 movement;
    int direction = 2; // 애니메이션 방향 (상:0, 우:1, 하:2, 좌:3)

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A, D
        float vertical = Input.GetAxisRaw("Vertical");     // W, S

        movement = new Vector2(horizontal, vertical);

        // 대각선 이동 시 수평 애니메이션 우선
        if (movement.x != 0) { // 필터링
            direction = (movement.x > 0) ? 1 : 3; // 우(1), 좌(3)
        } else if (movement.y != 0) {
            direction = (movement.y > 0) ? 0 : 2; // 상(0), 하(2)
        }

        animator.SetInteger("Direction", direction);
        animator.SetFloat("F_Direction", direction);
        animator.SetBool("IsMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
