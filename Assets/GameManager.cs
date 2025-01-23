using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤
    [Header("UI")]
    public GameObject dialoguePanel;
    GameObject interactionObject;
    public TextMeshProUGUI scanText;
    public TalkManager talkManager;
    public int talkIndex;
    public bool isDialogueActive = false;

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
        Talk(objData.id, objData.isNPC);
        dialoguePanel.SetActive(isDialogueActive);
    }

    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        scanText.text = interactionObject.name +": "+ talkData;

        if(talkData == null) {
            isDialogueActive = false;
            talkIndex = 0;
            return;
        }
        isDialogueActive = true;
        talkIndex++;
    }
}
