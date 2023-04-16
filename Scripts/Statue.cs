using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : InteractableObject
{
    [TextArea(1, 3)] public string[] missionSentences;

    private string npcName;
    private void Awake()
    {
        npcName = "���Ż�";
    }

    //  �÷��̾ ��ȣ�ۿ�� �̼� �߿��� �Ϸ���� ���� �̼��� ��ȯ�� ��ȭâ�� ����ϴ� �Լ�
    public override void Interaction()
    {
        int index = GameManager.instance.GetNotCompletedMission();
        UIManager.instance.StartDialog(ref npcName, ref missionSentences[index]);
    }
}
