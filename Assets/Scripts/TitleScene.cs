using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }

}
