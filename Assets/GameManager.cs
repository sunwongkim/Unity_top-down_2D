using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 싱글톤
    public GameObject dialoguePanel;
    public TextMeshProUGUI scanText;
    public bool isDialogueActive = false;

    void Awake()
    {
        // 싱글톤
        if  (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ScanObject(GameObject obj)
    {
        scanText.text = "오브젝트: " + obj.name;

        dialoguePanel.SetActive(!isDialogueActive);
        isDialogueActive = !isDialogueActive;
    }
}
