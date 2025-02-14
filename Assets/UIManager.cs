using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("DoTween")]
    public DOTweenAnimation Animation;
    public DOTweenPath path;
    public new string animation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Animation.DORestartAllById(animation);
            path?.DORestart();
        }
    }
}
