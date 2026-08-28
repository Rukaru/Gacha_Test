using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using UnityEngine;

public class GachaMethod
{
    // UUID
    private string _uuid;
    public string Uuid => _uuid;
    // sessionId
    private string _sessionId;
    public string SessionId => _sessionId;
    // デッキ詳細リスト
    private List<DeckDetail> _decks = new List<DeckDetail>();
    public List<DeckDetail> Decks => _decks;
    // ガチャIDリスト
    private List<int> _gachaID = new List<int>();
    public List<int> GachaID => _gachaID;

    /// <summary>
    /// UUID の取得
    /// </summary>
    /// <returns></returns>
    public async UniTask GetUUID()
    {
        try
        {
            // クライアントの生成
            using var client = new HttpClient();
            // UUID の取得
            var result = await client.GetAsync(CommonParams.URLGetUUID);
            // response の取得
            var response = await result.Content.ReadAsStringAsync();
            // 結果の Json テータをクラスに変換
            var method = JsonUtility.FromJson<UUIDMethod>(response);
            // 取得した値から uuid を保存する
            _uuid = method.response.uuid;
        }
        // どこかでエラーしたらここに来る
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async UniTask RegisterUUID(string userName)
    {
        try
        {
            // POST で渡すパラメータの設定
            var parameters = new Dictionary<string, string>()
            {
                { "uuid", _uuid },
                { "name", userName }
            };
            using var client = new HttpClient();
            await client.PostAsync(CommonParams.URLRegister, new FormUrlEncodedContent(parameters));
            //  ここは本来必要ないが、通信が終了したことを確認するために用意した
            Debug.Log("OK");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async UniTask GetSessionID()
    {
        try
        {
            var parameters = new Dictionary<string, string>()
            {
                { "uuid", _uuid }
            };
            using var client = new HttpClient();
            var result = await client.PostAsync(CommonParams.URLGetSession,
                                                new FormUrlEncodedContent(parameters));
            var response = await result.Content.ReadAsStringAsync();
            var method = JsonUtility.FromJson<SessionIDMethod>(response);
            _sessionId = method.response.session_id;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async UniTask GetGachaID(string deckID)
    {
        try
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                CommonParams.URLDrawLootBox(deckID)
            );

            // Authorizationをヘッダーに設定
            request.Headers.TryAddWithoutValidation(
                "Authorization",
                CommonParams.GetAuthorization(_sessionId)
            );

            var result = await client.SendAsync(request);

            var response = await result.Content.ReadAsStringAsync();

            Debug.Log($"ガチャAPI StatusCode: {result.StatusCode}");
            Debug.Log($"ガチャAPI Response: {response}");

            var method = JsonUtility.FromJson<GachaGetIDMethod>(response);

            if (method.response != null && method.response.card_ids != null)
            {
                _gachaID = method.response.card_ids;

                Debug.Log($"取得したカード数: {_gachaID.Count}");
            }
            else
            {
                Debug.LogError("カードIDがレスポンスにありません");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"ガチャエラー: {e.Message}");
        }
    }

    public async UniTask<Texture2D> GetCardImage(int cardID)
    {
        try
        {
            string url = CommonParams.URLGetImageDate(cardID.ToString());

            Debug.Log($"画像取得URL: {url}");

            using var client = new HttpClient();

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                url
            );

            request.Headers.TryAddWithoutValidation(
                "Authorization",
                CommonParams.GetAuthorization(_sessionId)
            );

            var result = await client.SendAsync(request);

            Debug.Log($"画像API StatusCode: {result.StatusCode}");

            if (!result.IsSuccessStatusCode)
            {
                string error = await result.Content.ReadAsStringAsync();

                Debug.LogError($"画像取得失敗: {result.StatusCode}");
                Debug.LogError($"画像API Response: {error}");

                return null;
            }

            byte[] imageData =
                await result.Content.ReadAsByteArrayAsync();

            Debug.Log($"画像データサイズ: {imageData.Length} bytes");

            Texture2D texture = new Texture2D(2, 2);

            if (!texture.LoadImage(imageData))
            {
                Debug.LogError("画像の読み込みに失敗しました");
                return null;
            }

            Debug.Log("カード画像取得成功！");

            return texture;
        }
        catch (Exception e)
        {
            Debug.LogError($"画像取得エラー: {e.Message}");
            return null;
        }
    }
}

