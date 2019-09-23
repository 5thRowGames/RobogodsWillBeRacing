using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Restart(string scene)
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(scene);
    }
}
