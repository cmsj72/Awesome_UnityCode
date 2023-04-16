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

    //  기둥에 불이 밝혀질 때 실행 밝혀진 기둥의 개수 증가시키는 함수
    //  MissionPillarSubject에서 NotifyObservers함수로 진입하면 실행되는 함수
    void IObserver.Update()
    {
        lightedCnt++;
        Debug.Log("기둥 증가 : " + lightedCnt);
    }

    //  기둥이 전부 밝혀졌는지 확인하는 함수
    public bool IsAllLighted()
    {
        return maxPillar == lightedCnt;
    }
}
