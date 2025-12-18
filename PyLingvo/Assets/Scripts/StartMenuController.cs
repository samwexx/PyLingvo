using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
   public void OnStartClick()
    {
        SceneManager.LoadScene("Scene 1");
    }


    public void OnExitClick()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }
}
