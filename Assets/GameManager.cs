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
    public bool isTalk = false;

    void Awake()
    {
        // 싱글톤
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);

        dialoguePanel.SetActive(false);
    }

    public void GetObject(GameObject obj)
    {
        interactionObject = obj;
        ObjData objData = obj.GetComponent<ObjData>();
        Talk(objData.ID, objData.NPC);
        dialoguePanel.SetActive(isTalk);
    }

    void Talk(int id, bool NPC)
    {
        string[] data = talkManager.GetTalk((TalkManager.ObjectID)id);

        // 대화가 끝나면 대화창 닫음
        if (talkIndex >= data.Length) {
            talkIndex = 0;
            isTalk = false;
            dialoguePanel.SetActive(false);
            return;
        }

        // 초상화
        if (interactionObject.name == "Box")
            // portraitImg.sprite = TalkManager.WOMAN_TALK;
            portraitImg.sprite = talkManager.portraitArr[0];
        else
            // portraitImg.sprite = TalkManager.MAN_TALK;
            portraitImg.sprite = talkManager.portraitArr[5];
        // 대사
        DialogueText.text = $"{interactionObject.name}: {data[talkIndex]}";

        // 배열이 안끝난 경우
        talkIndex++;
        isTalk = true;
        dialoguePanel.SetActive(true);
    }
}
