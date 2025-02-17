using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("DoTween")]
    public DOTweenAnimation Animation;
    public DOTweenPath path;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Animation.DORestart();
            path?.DORestart();
        }
    }
}
