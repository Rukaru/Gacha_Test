using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;



public class MainUIController : MonoBehaviour
{
    private Button _gachaButton;

    private GachaMethod _gachaMethod;
    private VisualElement _cardImage;

    [SerializeField] private EmissionContr _emissionContr;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _gachaButton = root.Q<Button>("GachaButton");
        _cardImage = root.Q<VisualElement>("CardImage");

        Debug.Log($"CardImage: {_cardImage}");

        _gachaButton.clicked += OnGachaButtonClicked;

        _gachaMethod = new GachaMethod();

        Initialize();
    }

    private void OnDisable()
    {
        _gachaButton.clicked -= OnGachaButtonClicked;
    }

    private async void Initialize()
    {
        await _gachaMethod.GetUUID();

        Debug.Log($"UUID取得: {_gachaMethod.Uuid}");

        await _gachaMethod.RegisterUUID("GachaPlayer");

        Debug.Log("ユーザー登録完了");

        await _gachaMethod.GetSessionID();

        Debug.Log($"SessionID取得: {_gachaMethod.SessionId}");
    }

    private async void OnGachaButtonClicked()
    {
        SoundManager.Instance.PlayButtonSE();

        Debug.Log("ガチャを引きます！");

        await _gachaMethod.GetGachaID("1");

        if (_gachaMethod.GachaID.Count == 0)
        {
            Debug.LogError("カードIDを取得できませんでした");
            return;
        }

        int cardID = _gachaMethod.GachaID[0];

        Debug.Log($"取得したCard ID: {cardID}");

        Texture2D texture = await _gachaMethod.GetCardImage(cardID);

        if (texture == null)
        {
            Debug.LogError("カード画像を取得できませんでした");
            return;
        }

        _cardImage.style.backgroundImage = Background.FromTexture2D(texture);

        SoundManager.Instance.PlayGachaSE();
        //_emissionContr.PlayEmission();
        Debug.Log("カード画像を表示した");
    }

    
}