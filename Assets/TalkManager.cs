// TalkManager.cs //
using UnityEngine;
using System;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public EventManager eventManager;
    public Sprite[] portraitArr;
    public class OBJECTID { public const int Box = 100, Desk = 200, Rock = 300, NpcMan = 1000, NpcWoman = 2000; }
    public static List<string> QuestNames = new List<string> {
        /*0*/"시작", /*1*/"첫인사", /*2*/"조언", /*3*/"선물", /*4*/"비밀 퀘스트", /*5*/"Q5"   
    };
    public List<int> targetObjects = new List<int>();
    public static Sprite MAN_IDLE, MAN_TALK, MAN_SMILE, MAN_ANGRY, WOMAN_IDLE, WOMAN_TALK, WOMAN_SMILE, WOMAN_ANGRY, PLAYER, OBJECT;
    Dictionary<int, Dictionary<int, List<Dialogue>>> talkData;

    void Awake()
    {
        MAN_IDLE = portraitArr[0]; MAN_TALK = portraitArr[1]; MAN_SMILE = portraitArr[2]; MAN_ANGRY = portraitArr[3]; WOMAN_IDLE = portraitArr[4]; WOMAN_TALK = portraitArr[5]; WOMAN_SMILE = portraitArr[6]; WOMAN_ANGRY = portraitArr[7]; PLAYER = portraitArr[8]; OBJECT = portraitArr[9];

        talkData = new Dictionary<int, Dictionary<int, List<Dialogue>>>() {
            { 0, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.NpcMan, new List<Dialogue> {
                    new Dialogue(MAN_TALK, "처음 보는군."),
                    new Dialogue(PLAYER, "옆 마을에서 왔어."),
                    new Dialogue(MAN_SMILE, "이방인은 오랜만이야."),}},
                { OBJECTID.NpcWoman, new List<Dialogue> {
                    new Dialogue(WOMAN_TALK, "안녕?"),
                    new Dialogue(WOMAN_SMILE, "..대답 안하니?"),
                    new Dialogue(WOMAN_SMILE, "야!!!", true, eventManager.npcAngry),
                    new Dialogue(PLAYER, "어.. 안녕.."),}},
                { OBJECTID.Box, new List<Dialogue> {
                    new Dialogue(OBJECT, "평범한 나무상자다."),}},
                { OBJECTID.Desk, new List<Dialogue> {
                    new Dialogue(OBJECT, "누군가 사용한 흔적이 있는 책상이다."),}},
                { OBJECTID.Rock, new List<Dialogue> {
                    new Dialogue(OBJECT, "돌멩이."),}},}
            },
            { 1, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.NpcWoman, new List<Dialogue> {
                    new Dialogue(WOMAN_ANGRY, "왜?"),
                    new Dialogue(PLAYER, "(나중에 다시 오자..)", true),}}}
            },
            { 2, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.NpcWoman, new List<Dialogue> {
                    new Dialogue(PLAYER, "안녕..?"),
                    new Dialogue(WOMAN_ANGRY, "저리가."),}},
                { OBJECTID.NpcMan, new List<Dialogue> {
                    new Dialogue(MAN_SMILE, "호되게 당하더군. 이 꽃을 갖다줘봐.", true),}}}
            },
            { 3, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.NpcWoman, new List<Dialogue> {
                    new Dialogue(PLAYER, "오다 주웠어. 받아."),
                    new Dialogue(WOMAN_SMILE, "고마워.", true),}}}
            },
            { 4, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Desk, new List<Dialogue> {
                    new Dialogue(PLAYER, "책상이 왜 여기있지?"),
                    new Dialogue(OBJECT, "만지지 마시게.", true),}}}
            },
            { 5, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Box, new List<Dialogue> {
                    new Dialogue(OBJECT, "QUEST 5", true),}}}
            },
        };
        
        // 퀘스트 대상
        targetObjects.Clear();
        foreach (var questState in talkData) {
            foreach (var objectid in questState.Value) {
                foreach (var dialogue in objectid.Value) {
                    if (dialogue.QuestProgress) {
                        targetObjects.Add(objectid.Key);
                        break;
                    }
                }
            }
        }
        Debug.Log("퀘스트 목표 목록: " + string.Join(", ", targetObjects));
    }

    public (List<Dialogue>, int) GetTalk(int id)
    {
        int currentQuest = GameManager.Instance.questState;
        // 최근 저장된 대사 탐색
        for (int i = currentQuest; i >= 0; i--) {
            if (talkData.TryGetValue(i, out var questData) && questData.TryGetValue(id, out var dialogues)) 
                return (dialogues, i);
        }
        return (null, -1);
    }
}

// 데이터 저장 클래스
public class Dialogue
{
    public Sprite Portrait { get; private set; }
    public string Text { get; private set; }
    public bool QuestProgress { get; private set; }
    public Action EventAction { get; private set; }

    public Dialogue(Sprite portrait, string text, bool questProgress = false, Action eventAction = null)
    {
        Portrait = portrait;
        Text = text;
        QuestProgress = questProgress;
        EventAction = eventAction;
    }
}
