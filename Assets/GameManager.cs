// GameManager.cs //
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤
    GameObject interactionObject;
    [Header("UI")]
    public GameObject dialoguePanel;
    public Image portraitImg;
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI QuestName;
    [Header("TALK")]
    public TalkManager talkManager;
    public int talkIndex;
    public bool isTalking = false;
    [Header("QUEST")]
    public int questState = 0;
    public GameObject QuestMarker;
    public Vector3 markerOffset = new Vector3(0, 0, 0);
    private Dictionary<int, ObjData> npcPosition = new Dictionary<int, ObjData>();

    void Awake()
    {
        if (Instance == null) { // 싱글톤
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);

        // NPC위치를 ID로 탐색
        foreach (ObjData npc in FindObjectsByType<ObjData>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            npcPosition[npc.ID] = npc;
        QuestMarker.transform.position = npcPosition[talkManager.QuestTargetObjects[questState]].transform.position + markerOffset;
        // UI
        dialoguePanel.SetActive(false);
        QuestName.text = $"{questState} : {TalkManager.QuestNames[questState]}";
    }

    public void Interaction(GameObject obj) // Space bar
    {
        interactionObject = obj;
        ObjData objData = obj.GetComponent<ObjData>();
        Talk(objData.ID);
        dialoguePanel.SetActive(isTalking);
        QuestMarker.SetActive(isTalking == false);
    }

    void Talk(int id)
    {
        var (data, currentQuest) = talkManager.GetTalk(id);
        // 대화끝
        if (data == null || talkIndex >= data.Count) {
            talkIndex = 0;
            isTalking = false;
            dialoguePanel.SetActive(false);
            FindFirstObjectByType<PlayerAction>().ResetObject();
            // 다음 퀘스트
            if (questState < TalkManager.QuestNames.Count)
                QuestName.text = $"{questState} : {TalkManager.QuestNames[questState]}";
            // 퀘스트 마커 위치
            if (questState < talkManager.QuestTargetObjects.Count)
                QuestMarker.transform.position = npcPosition[talkManager.QuestTargetObjects[questState]].transform.position + markerOffset;
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
        
        if ((currentQuest == questState) && dialogue.QuestProgress)
            questState++; // 다음 퀘스트

        // 대화중
        talkIndex++;
        isTalking = true;
        dialoguePanel.SetActive(true);
    }
}
