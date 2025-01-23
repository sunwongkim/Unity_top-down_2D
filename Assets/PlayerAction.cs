using UnityEngine;

public class PlayerAction : MonoBehaviour
{
  PlayerController playerController;
  // Ray
  [Header ("Ray")]
  Vector3 rayDirection;
  public float rayDistance = 2f;
  public Vector3 rayOffset = new Vector3(0, 0.7f, 0); // Ray 시작점
  public Color rayColor = Color.green;

  void Awake()
  {
    playerController = GetComponent<PlayerController>();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
      CheckObject();
  }

  void CheckObject()
  {
    Vector3 startPosition = ChangeDirection();
    // Raycast로 오브젝트 감지
    RaycastHit2D hit = Physics2D.Raycast(startPosition, rayDirection, rayDistance);

    if (hit.collider != null) {
      GameManager.Instance.GetObject(hit.collider.gameObject);
      Debug.Log($"오브젝트: {hit.collider.gameObject.name}");
    } else Debug.Log("없음");
  }

  void OnDrawGizmos() // 디버그. Gizmos는 이 함수 안에서만 작동
  {
    Vector3 startPosition = ChangeDirection();   
    Gizmos.DrawLine(startPosition, startPosition + rayDirection * rayDistance);
    Gizmos.color = rayColor;
  }

  Vector3 ChangeDirection()
  {
    Vector3 startPosition = transform.position; // 시작점

    switch (playerController?.direction) {
      case 0: // 상
        rayDirection = Vector3.up;
        startPosition += new Vector3(0, rayOffset.y, 0); break;
      case 1: // 우
        rayDirection = Vector3.right;
        startPosition += new Vector3(rayOffset.y, 0, 0); break;
      case 2: // 하
        rayDirection = Vector3.down;
        startPosition += new Vector3(0, -rayOffset.y, 0); break;
      case 3: // 좌
        rayDirection = Vector3.left;
        startPosition += new Vector3(-rayOffset.y, 0, 0); break;
      default: rayDirection = Vector3.down; break;
    }
    return startPosition;
  }
}

