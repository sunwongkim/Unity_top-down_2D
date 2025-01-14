using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTest : MonoBehaviour
{
    // &: Alt, #: Shift
    // Domain Reload - Ctrl+Alt+D
    [MenuItem("Reload/Domain Reload %&D")]
    public static void TriggerDomainReload()
    {
        Debug.Log("도메인 리로드 실행");
        CompilationPipeline.RequestScriptCompilation();
        Debug.Log("도메인 리로드 완료");
    }


    // Scene Reload - editor / Ctrl+Alt+E
    [MenuItem("Reload/Scene Reload - editor %&E")]
    public static void TriggerSceneReload_Editor()
    {
        Debug.Log("씬 리로드 실행");
        
        // 현재 활성화된 씬 로드
        string activeScenePath = EditorSceneManager.GetActiveScene().path;
        if (string.IsNullOrEmpty(activeScenePath))
        {
            Debug.LogError("현재 열려 있는 씬이 없습니다. 씬을 저장하고 다시 시도하세요!");
            return;
        }
        
        EditorSceneManager.OpenScene(activeScenePath); // 에디터에서 현재 씬 다시 로드
        Debug.Log("씬 리로드 완료");
    }


    // All Reload / Ctrl+Alt+A
    [MenuItem("Reload/All Reload - editor %&A")]
    public static void TriggerAllReload()
    {
        Debug.Log("도메인, 씬 리로드 실행");

        // 도메인 리로드
        Debug.Log("도메인 리로드 실행");
        CompilationPipeline.RequestScriptCompilation();
        Debug.Log("도메인 리로드 완료");
        
        // 씬 리로드
        Debug.Log("씬 리로드 실행");
        // 현재 활성화된 씬 로드
        string activeScenePath = EditorSceneManager.GetActiveScene().path;
        if (string.IsNullOrEmpty(activeScenePath))
        {
            Debug.LogError("현재 열려 있는 씬이 없습니다. 씬을 저장하고 다시 시도하세요");
            return;
        }
        
        EditorSceneManager.OpenScene(activeScenePath); // 에디터에서 현재 씬 다시 로드
        Debug.Log("씬 리로드 완료");
    }


    // Scene Reload - play mode / Ctrl+Alt+S
    [MenuItem("Reload/Scene Reload - play mode %&S")]
    public static void TriggerSceneReload_PLAYMODE()
    {
        Debug.Log("PLAY MODE 씬 리로드 실행");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 리로드
        Debug.Log("PLAY MODE 씬 리로드 완료");
    }
}