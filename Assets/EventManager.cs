// EventManager.cs //
using UnityEngine;
using DG.Tweening;

public class EventManager : MonoBehaviour
{
    PlayerController playerController;
    public DOTweenPath PathTween;
    public DOTweenAnimation AniTween;
    [Header("Not Tween")]
    public Transform target; // 이동시킬 오브젝트
    public Vector3 moveOffset; // 인스펙터에서 조절할 상대 좌표
    public float duration = 1f;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            PathTween?.DORestart();
            AniTween?.DORestart();
            target?.DOMove(target.position + moveOffset, duration); // 현재 위치 기준 이동
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

    public void GameStart()
    {
        SetMonologue();
    }

    public void SetMoving(bool state)
    {
        GameManager.Instance.isEvent = state;
        playerController.animator.SetBool("IsMoving", state);
    }

        public void SetDirection(int i)
    {
        playerController.direction = i;
        playerController.animator.SetInteger("Direction", i);
        playerController.animator.SetFloat("F_Direction", i);
    }
}
