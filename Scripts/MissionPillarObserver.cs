using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MissionPillarObserver : IObserver
{
    private int lightedCnt;
    private readonly int maxPillar;

    public MissionPillarObserver(int maxPillar)
    {
        lightedCnt = 0;
        this.maxPillar = maxPillar;
    }

    //  ��տ� ���� ������ �� ���� ������ ����� ���� ������Ű�� �Լ�
    //  MissionPillarSubject���� NotifyObservers�Լ��� �����ϸ� ����Ǵ� �Լ�
    void IObserver.Update()
    {
        lightedCnt++;
        Debug.Log("��� ���� : " + lightedCnt);
    }

    //  ����� ���� ���������� Ȯ���ϴ� �Լ�
    public bool IsAllLighted()
    {
        return maxPillar == lightedCnt;
    }
}
