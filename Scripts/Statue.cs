using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : InteractableObject
{
    [TextArea(1, 3)] public string[] missionSentences;

    private string npcName;
    private void Awake()
    {
        npcName = "여신상";
    }

    //  플레이어가 상호작용시 미션 중에서 완료되지 않은 미션을 반환해 대화창에 출력하는 함수
    public override void Interaction()
    {
        int index = GameManager.instance.GetNotCompletedMission();
        UIManager.instance.StartDialog(ref npcName, ref missionSentences[index]);
    }
}
