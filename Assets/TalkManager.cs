// TalkManager.cs //
using UnityEngine;
using System;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public EventManager eventManager;
    public Sprite[] portraitArr;
    public class OBJECTID { public const int Player = 1, Inn = 100, Desk = 200, Rock = 300, Cave = 400, Trader_Woman = 1000, Trader_Man = 2000, Citizen_Woman = 3000, Monster = 4000; }
    public static List<string> QuestNames = new List<string> {
        /*0*/"도를 전파하자", /*1*/"첫인사", /*2*/"조언", /*3*/"선물", /*4*/"비밀 퀘스트", /*5*/"Q5"   
    };
    public List<int> QuestTargetObjects = new List<int>();
    public static Sprite MAN_IDLE, MAN_TALK, MAN_SMILE, MAN_ANGRY, WOMAN_IDLE, WOMAN_TALK, WOMAN_SMILE, WOMAN_ANGRY, PLAYER, OBJECT;
    Dictionary<int, Dictionary<int, List<Dialogue>>> talkData;

    void Awake()
    {
        MAN_IDLE = portraitArr[0]; MAN_TALK = portraitArr[1]; MAN_SMILE = portraitArr[2]; MAN_ANGRY = portraitArr[3]; WOMAN_IDLE = portraitArr[4]; WOMAN_TALK = portraitArr[5]; WOMAN_SMILE = portraitArr[6]; WOMAN_ANGRY = portraitArr[7]; PLAYER = portraitArr[8]; OBJECT = portraitArr[9];

        talkData = new Dictionary<int, Dictionary<int, List<Dialogue>>>() {
            { 0, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Player, new List<Dialogue> {
                    new Dialogue(PLAYER, "내 이름은 알렉스. 천황교의 수석 선교사다."),
                    new Dialogue(PLAYER, "이전 마을의 선교는 성공했다. 이젠 이 마을 차례군."),}},
                { OBJECTID.Trader_Woman, new List<Dialogue> {
                    new Dialogue(WOMAN_SMILE, "손님 어서오세요! 좋은 물건이 많이 들어왔답니다!"),
                    new Dialogue(WOMAN_SMILE, "아 그런거군요! 그럼요!", eventManager.Quest0/*화면전환-10시간후 -> 밤*/),
                    new Dialogue(WOMAN_SMILE, "그래서 이 물건으로 말씀드리자면~"),
                    new Dialogue(WOMAN_SMILE, "감사합니다! 다음에도 부탁드릴게요!", null, true),}},
                { OBJECTID.Trader_Man, new List<Dialogue> {
                    new Dialogue(MAN_SMILE, "장사 준비중 입니다."),}},
                { OBJECTID.Citizen_Woman, new List<Dialogue> {
                    new Dialogue(WOMAN_SMILE, "꺄르르르륵."),}},
                { OBJECTID.Inn, new List<Dialogue> {
                    new Dialogue(OBJECT, "여관"),}},
                { OBJECTID.Desk, new List<Dialogue> {
                    new Dialogue(OBJECT, "책상 위는 난잡하다."),}},
                { OBJECTID.Rock, new List<Dialogue> {
                    new Dialogue(OBJECT, "동굴은 바위로 막혀있다."),}},}
            },
            { 1, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Inn, new List<Dialogue> {
                    new Dialogue(PLAYER, "밤이 늦었다.. 오늘은 쉬자..", eventManager.Quest1/*화면전환-다음날 -> 낮*/, true),}}}
            },
            { 2, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Trader_Man, new List<Dialogue> {
                    new Dialogue(MAN_IDLE, "안살거면 가쇼."),
                    // 선교성공?
                    new Dialogue(PLAYER, "잠시 얘기 좀 나눠 보시지요.", eventManager.Quest2/*화면전환-잠시후*/),
                    // 상인의 제안
                    new Dialogue(MAN_TALK, "저기있는 책상에 갈색 노트를 가져다주게.", null, true),}},}
            },
            { 3, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Desk, new List<Dialogue> {
                    new Dialogue(PLAYER, "이 노트인 것 같군. 제목이.. 루나의 일기..? [이벤트: 여자 뛰어옴]", eventManager.Quest3_1),
                    new Dialogue(WOMAN_ANGRY, "지금 뭐하시는 거에요!! [이벤트: 펄쩍뜀]", eventManager.Quest3_2),
                    new Dialogue(WOMAN_ANGRY, "변명하지마!!! [이벤트: 공격]", eventManager.Quest3_3, true),}}}
            },
            { 4, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Trader_Man, new List<Dialogue> {
                    // 상인에게 속인것을 따진다
                    new Dialogue(PLAYER, "그냥 넘어가려고 하지마시오! [이벤트: 여자상인 등장]", eventManager.Quest4_1),
                    // 주인공의 변명
                    new Dialogue(WOMAN_ANGRY, "또 너야? 이번엔 이방인을 시켜?! [이벤트: 남자상인을 공격]", eventManager.Quest4_2),
                    new Dialogue(WOMAN_ANGRY, "..외상은 꼭 갚도록 하세요.", null, true),}}}
            },
            { 5, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Citizen_Woman, new List<Dialogue> {
                    new Dialogue(PLAYER, "하.. 이 마을은 쉽지 않군.. 시민에게 선교를 해야겠어."),
                    // 촌장의 존재
                    new Dialogue(WOMAN_TALK, "어차피 막혀있어서 들어가실 수 없을테니 회관에서 며칠 기다려 보세요.", null, true),}}}
            },
            { 6, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Rock, new List<Dialogue> {
                    new Dialogue(PLAYER, "강매당한 황금 망치. 어떤 돌이든 깨부순다고 했지. 속는셈치고 한번 써볼까. [이벤트: 바위 파괴]", eventManager.Quest6),
                    new Dialogue(PLAYER, "거짓말이 아니었어???", null, true),}}}
            },
            { 7, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Cave, new List<Dialogue> {
                    new Dialogue(PLAYER, "마을의 주교인 촌장.. 기대되는군. [이벤트: 맵이동]", eventManager.Quest7, true),}}}
            },
            { 8, new Dictionary<int, List<Dialogue>>() {
                { OBJECTID.Monster, new List<Dialogue> { // 이벤트 트리거 필요(플레이어가 동굴 깊이 들어감)
                    new Dialogue(OBJECT, "분명 들어오지 말라고 했을텐데.. 친히 입구까지 막아놓았거늘.. [이벤트: 괴물 등장]"),
                    new Dialogue(PLAYER, "이방인이 길을 잘못 들었습니다. 가보겠습니다. [이벤트: 괴물이 입구를 막는다.]", eventManager.Quest8_1),
                    // 괴물 설득 실패
                    new Dialogue(OBJECT, "... [이벤트: 화면 어두워지고 엔딩 모드]", eventManager.Quest8_2),
                    new Dialogue(PLAYER, "독백 [이벤트: 게임클리어]", eventManager.Quest8_3),}}}
            },
        };
        
        // 퀘스트 대상
        QuestTargetObjects.Clear();
        foreach (var questState in talkData) {
            foreach (var objectid in questState.Value) {
                foreach (var dialogue in objectid.Value) {
                    if (dialogue.QuestProgress) {
                        QuestTargetObjects.Add(objectid.Key);
                        break;
                    }
                }
            }
        }
        Debug.Log("퀘스트 목표 목록: " + string.Join(", ", QuestTargetObjects));
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

    public Dialogue(Sprite portrait, string text, Action eventAction = null, bool questProgress = false)
    {
        Portrait = portrait;
        Text = text;
        EventAction = eventAction;
        QuestProgress = questProgress;
    }
}
