using UnityEngine;
using System;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public EventManager eventManager;
    public Sprite[] portraitArr;
    public enum OBJECTID { Box = 100, Desk = 200, Rock = 300, NpcMan = 1000, NpcWoman = 2000 }
    public static Sprite MAN_IDLE, MAN_TALK, MAN_SMILE, MAN_ANGRY, WOMAN_IDLE, WOMAN_TALK, WOMAN_SMILE, WOMAN_ANGRY, PLAYER, OBJECT;
    Dictionary<OBJECTID, List<Dialogue>> talkData;

    void Awake()
    {
        MAN_IDLE = portraitArr[0]; MAN_TALK = portraitArr[1]; MAN_SMILE = portraitArr[2]; MAN_ANGRY = portraitArr[3]; WOMAN_IDLE = portraitArr[4]; WOMAN_TALK = portraitArr[5]; WOMAN_SMILE = portraitArr[6]; WOMAN_ANGRY = portraitArr[7]; PLAYER = portraitArr[8]; OBJECT = portraitArr[9];

        talkData = new Dictionary<OBJECTID, List<Dialogue>>() {
            // 기본 대사
            { OBJECTID.NpcMan, new List<Dialogue> {
                new Dialogue(MAN_TALK, "처음 보는군."),
                new Dialogue(PLAYER, "옆 마을에서 왔어."),
                new Dialogue(MAN_SMILE, "이방인은 오랜만이야."),}},
            { OBJECTID.NpcWoman, new List<Dialogue> {
                new Dialogue(WOMAN_TALK, "안녕?"),
                new Dialogue(WOMAN_SMILE, "..대답 안하니?"),
                new Dialogue(WOMAN_ANGRY, "야!!!", true, eventManager.npcAngry),
                new Dialogue(PLAYER, "응.. 안녕.."),}},
            { OBJECTID.Box, new List<Dialogue> { new Dialogue(OBJECT, "평범한 나무상자다.")}},
            { OBJECTID.Desk, new List<Dialogue> { new Dialogue(OBJECT, "누군가 사용한 흔적이 있는 책상이다.")}},
            { OBJECTID.Rock, new List<Dialogue> { new Dialogue(OBJECT, "돌멩이.")}},
            
            // quest 1: [첫인사]
            { OBJECTID.NpcWoman + 1, new List<Dialogue> {
                new Dialogue(WOMAN_ANGRY, "왜?"),
                new Dialogue(PLAYER, "(나중에 다시 오자..)", true),}},
            
            // quest 2: [조언]
            { OBJECTID.NpcWoman + 2, new List<Dialogue> { // *퀘스트 영향 없음*
                new Dialogue(PLAYER, "안녕..?"),
                new Dialogue(WOMAN_ANGRY, "저리가."),}},
            { OBJECTID.NpcMan + 2, new List<Dialogue> {
                new Dialogue(MAN_SMILE, "호되게 당하더군. 이 꽃을 갖다줘봐.", true),}}, // 버그:*questState가 3이 넘어가도 말걸때마다 1씩 올라감.*

            // quest 3: [선물]
            { OBJECTID.NpcWoman + 3, new List<Dialogue> {
                new Dialogue(PLAYER, "오다 주웠어. 받아."),
                new Dialogue(WOMAN_SMILE, "응.."),}},

            { OBJECTID.Desk + 4, new List<Dialogue> { new Dialogue(OBJECT, "quest 4")}},
            { OBJECTID.Desk + 5, new List<Dialogue> { new Dialogue(OBJECT, "quest 5")}},
        };
    }

    public List<Dialogue> GetTalk(OBJECTID id)
    {
        int questState = GameManager.Instance.questState;
        // 최근 저장된 대사 탐색
        for (int i = questState; i >= 0; i--) {
            if (talkData.ContainsKey(id + i))
                return talkData[id + i];
        }
        return talkData.ContainsKey(id) ? talkData[id] : null;
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
