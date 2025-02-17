// PlayerAction.cs //
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
  PlayerController playerController;
  public GameObject noneObject;
  public GameObject currentObject;
  [Header ("Ray")]
  Vector3 rayDirection;
  public float rayDistance = 2f;
  public Vector3 rayOffset = new Vector3(0, 0.7f, 0); // Ray 시작점
  public Color rayColor = Color.green;

  void Awake()
  {
    playerController = GetComponent<PlayerController>();
    noneObject = new GameObject("NoneObject");
    currentObject = noneObject;
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
      if ((currentObject == noneObject) && (hit.collider.GetComponent<ObjData>() != null)) {
        currentObject = hit.collider.gameObject;
      } else if (currentObject != noneObject) { // 전방 오브젝트 없어져도 대화 유지
        Debug.Log("대화 유지: currentObject != noneObject");
      }
      Debug.Log("스캔: " + hit.collider.gameObject.name);
      Debug.Log("대화중: " + currentObject.name);
    } else {
      Debug.Log("hit.collider == null");
    }
    GameManager.Instance.Interaction(currentObject);
  }

  public void ResetObject() => currentObject = noneObject;

  public void PlayerObject(GameObject obj) => currentObject = obj;

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
