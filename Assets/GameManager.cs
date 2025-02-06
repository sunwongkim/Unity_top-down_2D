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
    [Header("QUEST")]
    public int questState;

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
        Talk(objData.ID);
        dialoguePanel.SetActive(isTalking);
    }

    void Talk(int id)
    {
        var (currentQuest, data) = talkManager.GetTalk(id); // 여기
        
        // 대화끝
        if (data == null || talkIndex >= data.Count) {
            talkIndex = 0;
            isTalking = false;
            dialoguePanel.SetActive(false);
            return;
        }

        Dialogue dialogue = data[talkIndex];

        // 초상화
        portraitImg.sprite = dialogue.Portrait;
        portraitImg.gameObject.SetActive((dialogue.Portrait != TalkManager.PLAYER) && (dialogue.Portrait != TalkManager.OBJECT));

        // 대사
        if (dialogue.Portrait == TalkManager.PLAYER)
            DialogueText.text = $"플레이어: {dialogue.Text}";
        else
            DialogueText.text = $"{interactionObject.name}: {dialogue.Text}";

        dialogue.EventAction?.Invoke(); // 이벤트 실행
        
        if (currentQuest == questState && dialogue.QuestProgress )
            questState++; // 다음 퀘스트

        // 대화중
        talkIndex++;
        isTalking = true;
        dialoguePanel.SetActive(true);
    }
}
