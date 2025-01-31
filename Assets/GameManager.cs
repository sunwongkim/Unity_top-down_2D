using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤
    GameObject interactionObject;
    [Header("UI")]
    public GameObject dialoguePanel;
    public Image portraitImg;
    public TextMeshProUGUI DialogueText;
    [Header("TALK")]
    public TalkManager talkManager;
    public int talkIndex;
    public bool isTalking = false;

    void Awake()
    {
        if (Instance == null) { // 싱글톤
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);

        dialoguePanel.SetActive(false);
    }

    public void Interaction(GameObject obj) // Space bar
    {
        interactionObject = obj;
        ObjData objData = obj.GetComponent<ObjData>();
        Talk(objData.ID, objData.NPC);
        dialoguePanel.SetActive(isTalking);
    }

    void Talk(int ID, bool NPC)
    {
        var data = talkManager.GetTalk((TalkManager.ObjectID)ID);

        // 대사 끝난 경우
        if (talkIndex >= data.Count) {
            talkIndex = 0;
            isTalking = false;
            dialoguePanel.SetActive(false);
            return;
        }
        
        var (portrait, dialogue) = data[talkIndex]; // 반드시 여기 위치
        
        // 초상화
        if ((portrait != TalkManager.PLAYER) || (portrait != null)){
            portraitImg.sprite = portrait;
            portraitImg.gameObject.SetActive(true);
        } else portraitImg.gameObject.SetActive(false);

        // 대사
        if (portrait == TalkManager.PLAYER) {
            DialogueText.text =  $"플레이어: {dialogue}";
        } else
            DialogueText.text = $"{interactionObject.name}: {dialogue}";

        // 대사 남은 경우
        talkIndex++;
        isTalking = true;
        dialoguePanel.SetActive(true);
    }
}
