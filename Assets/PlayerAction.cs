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

  public void CheckObject()
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
      Debug.Log($"스캔: {hit.collider.gameObject.name} / 대화중: {currentObject.name}");
    } else {
      Debug.Log("hit.collider == null");
    }
    GameManager.Instance.Interaction(currentObject);
  }

  public void ResetObject() => currentObject = noneObject;

  public void SetObject(GameObject obj) => currentObject = obj;

  void OnDrawGizmos() // 디버그. Gizmos는 이 함수 안에서만 작동
  {
    Vector3 startPosition = ChangeDirection();   
    Gizmos.DrawLine(startPosition, startPosition + rayDirection * rayDistance);
    Gizmos.color = rayColor;
  }

  Vector3 ChangeDirection()
  {
    Vector3 startPosition = transform.position; // Ray 시작점
    rayDirection = playerController?.direction switch {
      0 => Vector3.up,
      1 => Vector3.right,
      2 => Vector3.down,
      3 => Vector3.left,
      _ => Vector3.down
    };
    return startPosition + rayDirection * rayOffset.y;
  }
}
