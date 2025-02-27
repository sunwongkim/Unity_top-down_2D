// PlayerController.cs //
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerAction playerAction;
    Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public float moveSpeed;
    public int direction = 2; // 애니메이션 방향 (상:0, 우:1, 하:2, 좌:3)

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAction = GetComponent<PlayerAction>();
    }

    void Update()
    {
        // ESC 메뉴 코드 여기에 짜면됨.
        if (GameManager.Instance.isEvent == true)
            return; 

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.Instance.ExecuteEvent();
            playerAction.CheckObject();
        }

        // 대화창이 없을 때만 조작 가능
        if (GameManager.Instance.isTalking == false) {
            float horizontal = Input.GetAxisRaw("Horizontal"); // A, D
            float vertical = Input.GetAxisRaw("Vertical");     // W, S
            movement = new Vector2(horizontal, vertical);
        } else movement = new Vector2(0, 0);

        // 대각선 이동 시 수평 방향 우선
        if (movement.x != 0)
            direction = (movement.x > 0) ? 1 : 3; // 우(1), 좌(3)
        else if (movement.y != 0)
            direction = (movement.y > 0) ? 0 : 2; // 상(0), 하(2)

        animator.SetInteger("Direction", direction); // Legacy
        animator.SetFloat("F_Direction", direction); // Blend
        animator.SetBool("IsMoving", movement != Vector2.zero);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
