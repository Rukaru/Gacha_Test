using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour

{

    public static FadeManager Instance { get; private set; }

    private Canvas _canvas;

    private Image _fadeImage;

    [SerializeField] private float _fadeDuration = 0.5f;

    private void Awake()

    {
        // ÉVÉìÉOÉãÉgÉì
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);

            return;

        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateFadeUI();
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private void CreateFadeUI()
    {
        // CanvasçÏê¨
        GameObject canvasObject = new GameObject("FadeCanvas");
        canvasObject.transform.SetParent(transform);

        _canvas = canvasObject.AddComponent<Canvas>();

        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _canvas.sortingOrder = 100;

        canvasObject.AddComponent<CanvasScaler>();

        canvasObject.AddComponent<GraphicRaycaster>();

        // ImageçÏê¨

        GameObject imageObject = new GameObject("FadeImage");

        imageObject.transform.SetParent(canvasObject.transform, false);

        _fadeImage = imageObject.AddComponent<Image>();

        // âÊñ Ç¢Ç¡ÇœÇ¢

        RectTransform rect = _fadeImage.rectTransform;

        rect.anchorMin = Vector2.zero;

        rect.anchorMax = Vector2.one;

        rect.offsetMin = Vector2.zero;

        rect.offsetMax = Vector2.zero;

        // çï

        _fadeImage.color = Color.black;

    }

    private IEnumerator FadeIn()

    {

        yield return Fade(1f, 0f);

    }

    private IEnumerator FadeOut()

    {

        yield return Fade(0f, 1f);

    }

    private IEnumerator Fade(float startAlpha, float endAlpha)

    {

        float time = 0f;

        Color color = _fadeImage.color;

        color.a = startAlpha;

        _fadeImage.color = color;

        while (time < _fadeDuration)

        {

            time += Time.deltaTime;

            float t = time / _fadeDuration;

            color.a = Mathf.Lerp(startAlpha, endAlpha, t);

            _fadeImage.color = color;

            yield return null;

        }

        color.a = endAlpha;

        _fadeImage.color = color;

    }

    public void LoadScene(string sceneName)

    {

        StartCoroutine(FadeOutAndLoad(sceneName));

    }

    private IEnumerator FadeOutAndLoad(string sceneName)

    {

        yield return FadeOut();

        SceneManager.LoadScene(sceneName);

    }

}
