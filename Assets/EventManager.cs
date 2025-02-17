using UnityEngine;
using DG.Tweening;

public class EventManager : MonoBehaviour
{
    public DOTweenPath PathTween;
    public DOTweenAnimation AniTween;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            PathTween?.DORestart();
            AniTween?.DORestart();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            SetMonologue();
        }
    }
    public void npcAngry()
    {
        Debug.Log("NPC가 화를 냄!");
    }

    public void PlayAniTween()
    {
        Debug.Log("PlayAniTween");
        AniTween?.DORestart();
    }

    public void SetMonologue()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            GameManager.Instance.Interaction(player);
            FindFirstObjectByType<PlayerAction>().PlayerObject(player);
        } else
            Debug.Log("Player == null");
    }
}
