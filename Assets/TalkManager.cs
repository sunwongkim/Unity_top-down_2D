using UnityEngine;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public Sprite[] portraitArr;
    public enum ObjectID{Box = 100, Desk = 200, Rock = 300, NpcMan = 1000, NpcWoman = 2000}
    public static Sprite MAN_IDLE, MAN_TALK, MAN_SMILE, MAN_ANGRY, WOMAN_IDLE, WOMAN_TALK, WOMAN_SMILE, WOMAN_ANGRY;
    Dictionary<ObjectID, string[]> talkData;
    void Awake()
    {
        { MAN_IDLE = portraitArr[0]; MAN_TALK = portraitArr[1]; MAN_SMILE = portraitArr[2]; MAN_ANGRY = portraitArr[3]; WOMAN_IDLE = portraitArr[0]; WOMAN_TALK = portraitArr[1]; WOMAN_SMILE = portraitArr[2]; WOMAN_ANGRY = portraitArr[3]; }
        talkData = new Dictionary<ObjectID, string[]>
        {
            { ObjectID.NpcMan, new string[] { "처음 보는군.", "이방인은 오랜만이야." } },
            { ObjectID.NpcWoman, new string[] { "안녕?", "..대답 안하니?", "야!!!" } },
            { ObjectID.Box, new string[] { "평범한 나무상자다." } },
            { ObjectID.Desk, new string[] { "누군가 사용한 흔적이 있는 책상이다." } },
            { ObjectID.Rock, new string[] { "돌멩이." } }
        };
    }

    public string[] GetTalk(ObjectID id)
    {
        return talkData.ContainsKey(id) ? talkData[id] : null;
    }
}
