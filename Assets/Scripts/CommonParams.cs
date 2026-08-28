using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CommonParams
{
    public static string URLBasics = "https://lootbox2.gjmj.net";
    public static string URLGetUUID = $"{URLBasics}/uuid";
    //　register（名前）
    public static string URLRegister = $"{URLBasics}/register";
    public static string URLGetSession = $"{URLBasics}/session/get";
    public static string URLGetLootBoxList = $"{URLBasics}/loot_box/list";
    public static string URLGetTakeList = $"{URLBasics}/card/list";
    public static string GetAuthorization(string sessionId)
     => $"Bearer {sessionId}";
    public static string URLDrawLootBox(string deckId) => $"{URLBasics}/loot_box/draw/{deckId}";
    public static string URLGetCardDetail(string cardId) => $"{URLBasics}/card/detail/{cardId}";
    public static string URLGetImageDate(string cardId) => $"{URLBasics}/card/image/{cardId}";
}

//
//
[System.Serializable]
public class UUIDMethod
{
    [System.Serializable]
    public class UuidStatus
    {
        public string uuid;
    }
    public UuidStatus response;
    public int status_code;
}

//  session を取得する
[System.Serializable]
public class SessionIDMethod
{
    [System.Serializable]
    public class SessionParam
    {
        public string session_id;
    }
    public SessionParam response;
    public int status_code;
}

[System.Serializable]
public class DeckDetail
{
    public string id;
    public string name;
    public string detail;
    public bool can_loot;
}

[System.Serializable]
public class DecksMethod
{
    [System.Serializable]
    public class DeckMethod
    {
        public List<DeckDetail> decks;
    }
    public DeckMethod response;
    public int status_code;
}

// デッキIDからカードを取得（10レンなら配列に10個はいる）
[System.Serializable]
public class GachaGetIDMethod
{
    [System.Serializable]
    public class GachaGetID
    {
        public List<int> card_ids;
    }
    public GachaGetID response;
    public int status_code;
}

[System.Serializable]
public class TakeCardMethod
{
    [System.Serializable]
    public class CardParam
    {
        public string card_id;
        public int quantity;
    }
    [System.Serializable]
    public class CardMethod
    {
        public List<CardParam> cards;
    }
    public CardMethod response;
    public int status_code;
}

// カード情報の格納
[System.Serializable]
public class CardInfoMethod
{
    [System.Serializable]
    public class CardInfo
    {
        public string card_id;
        public string caed_name;
        public int offense;
        public int defense;
        public string description;
    }
    public CardInfo response;
    public int status_code;
}

// イメージのバイナリデータを取得する
[System.Serializable]
public class CardImageMethod
{
    [System.Serializable]
    public class CardImage
    {
        public byte[] image;
    }
    public CardImage response;
    public int status_code;
}

