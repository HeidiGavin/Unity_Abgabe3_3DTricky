using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
