using UnityEngine;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public Sprite[] portraitArr;
    public enum ObjectID { Box = 100, Desk = 200, Rock = 300, NpcMan = 1000, NpcWoman = 2000 }
    public static Sprite MAN_IDLE, MAN_TALK, MAN_SMILE, MAN_ANGRY, WOMAN_IDLE, WOMAN_TALK, WOMAN_SMILE, WOMAN_ANGRY, PLAYER, OBJECT;
    Dictionary<ObjectID, List<(Sprite, string)>> talkData;

    void Awake()
    {
        MAN_IDLE = portraitArr[0]; MAN_TALK = portraitArr[1]; MAN_SMILE = portraitArr[2]; MAN_ANGRY = portraitArr[3]; WOMAN_IDLE = portraitArr[4]; WOMAN_TALK = portraitArr[5]; WOMAN_SMILE = portraitArr[6]; WOMAN_ANGRY = portraitArr[7]; PLAYER = portraitArr[8]; OBJECT = portraitArr[9];

        talkData = new Dictionary<ObjectID, List<(Sprite, string)>>() {
            // 기본 대사
            { ObjectID.NpcMan, new List<(Sprite, string)> {
                (MAN_TALK, "처음 보는군."),
                (PLAYER, "옆 마을에서 왔어."),
                (MAN_SMILE, "이방인은 오랜만이야."),}},
            { ObjectID.NpcWoman, new List<(Sprite, string)> {
                (WOMAN_TALK, "안녕?"),
                (WOMAN_SMILE, "..대답 안하니?"),
                (WOMAN_ANGRY, "야!!!"),
                (PLAYER, "응.. 안녕.."),}},
            { ObjectID.Box, new List<(Sprite, string)> {(OBJECT, "평범한 나무상자다.")}},
            { ObjectID.Desk, new List<(Sprite, string)> {(OBJECT, "누군가 사용한 흔적이 있는 책상이다.")}},
            { ObjectID.Rock, new List<(Sprite, string)> {(OBJECT, "돌멩이.")}},
            
            // quest 1: [첫인사]
            { ObjectID.NpcWoman + 1, new List<(Sprite, string)> {
                (WOMAN_ANGRY, "왜?"),
                (PLAYER, "(나중에 다시 오자..)"),}},
            
            // quest 2: [조언]
            { ObjectID.NpcWoman + 2, new List<(Sprite, string)> { // 퀘스트 영향 없음
                (PLAYER, "안녕..?"),
                (WOMAN_ANGRY, "저리가."),}},
            { ObjectID.NpcMan + 2, new List<(Sprite, string)> {
                (MAN_SMILE, "호되게 당하더군. 이 꽃을 갖다줘봐."),}},

            // quest 3: [선물]
            { ObjectID.NpcWoman + 3, new List<(Sprite, string)> {
                (PLAYER, "오다 주웠어. 받아."),
                (WOMAN_SMILE, "응.."),}},
        };
    }

    public List<(Sprite, string)> GetTalk(ObjectID id)
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
