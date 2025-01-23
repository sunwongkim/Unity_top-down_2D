using UnityEngine;
using System.Collections.Generic;

public class TalkManager : MonoBehaviour
{
    public const int BOX = 100;
    public const int DESK = 200;
    public const int ROCK = 300;
    public const int NPC_MAN = 1000;
    public const int NPC_WOMAN = 2000;
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>
        {
            { NPC_MAN, new string[] { "M: 첫번째 대사", "그 다음 대사" } },
            { NPC_WOMAN, new string[] { "1/3", "2/3", "3/3" } },
            { BOX, new string[] { "평범한 나무상자다." } },
            { DESK, new string[] { "누군가 사용한 흔적이 있는 책상이다." } },
            { ROCK, new string[] { "돌멩이." } }
        };
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex >= talkData[id].Length)
            return null;
        return talkData[id][talkIndex];
    }
}
