using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    public float rotationSpeed; // 회전 속도
    [SerializeField] private float fixedRotationSpeed; // 회전 속도

    void Update()
    {
        // Y축을 기준으로 회전
        transform.Rotate(0, rotationSpeed * Time.deltaTime*1, 0);
        // transform.Rotate(0, 0, fixedRotationSpeed * Time.deltaTime * 1000);
        // transform.Rotate(Time.deltaTime * 1000, 0, 0);
    }
}
