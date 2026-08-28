using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScene : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainScene");
        ////SoundManager.Instance.PlayButtonSE();
        //if (FadeManager.Instance == null)
        //{
        //    Debug.LogError("null!!");
        //    return;
        //}
        //FadeManager.Instance.LoadScene("MainScene");
        //Debug.Log("Ÿ‚ÌƒV[ƒ“‚Ö");
    }

}
