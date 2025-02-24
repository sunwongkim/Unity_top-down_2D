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
            GameStart();
        }
    }

    public void PlayAniTween()
    {
        Debug.Log("PlayAniTween");
        AniTween?.DORestart();
    }

    public void Quest0() {
        HighlightText("6시간 후");
        Debug.Log("화면 어둡게");
    }
    public void Quest1() {
        HighlightText("");
        Debug.Log("화면 밝게");
    }

    public void Quest2() {
        HighlightText("잠시 후");
    }

    public void Quest3_1() {
        Debug.Log("여자 뛰어옴(path)");
    }

    public void Quest3_2() {
        Debug.Log("펄쩍뜀");
    }

    public void Quest3_3() {
        Debug.Log("공격");
        Debug.Log("날라감");
        Debug.Log("플레이어 쓰러짐");
        Debug.Log("여관으로이동, 밖으로 나옴");
    }

    public void Quest4_1() {
        Debug.Log("여자 다가옴(path)");
    }

    public void Quest4_2() {
        Debug.Log("남자상인 공격");
        Debug.Log("남자상인 날라감");
    }

    public void Quest6() {
        Debug.Log("바위파괴");
    }

    public void Quest7() {
        Debug.Log("맵이동");
        HighlightText("");
    }

    public void Quest8_1() {
        Debug.Log("입구막음");
    }

    public void Quest8_2() {
        Debug.Log("해설띄움");
    }
    public void Quest8_3() {
        Debug.Log("클리어");
    }

    public string HighlightText(string text)
    {
        Debug.Log(text);
        return text;
    }

    public void setDark()
    {
        Debug.Log("화면 어두워짐");
    }

    public void setBright()
    {
        Debug.Log("화면 밝아짐");
    }

    public void GameStart()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            GameManager.Instance.Interaction(player);
            FindFirstObjectByType<PlayerAction>().SetObject(player);
        } else
            Debug.Log("Player == null");
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
