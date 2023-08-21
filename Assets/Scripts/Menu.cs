using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private int sceneNum;
    
    public void GoToScene()
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
