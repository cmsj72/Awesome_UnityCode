using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance = null;
    private GameObject[] chestObjects;

    private MissionPillarObserver missionPillarObserver;
    private bool[] missionCheck;
    private int maxCrystalCnt;
    private int crystalCnt;
    private bool isStop;

    public bool IsStop
    {
        get { return isStop; }
        set { isStop = value; }
    }

    public MissionPillarObserver propMissionPillarObserver
    {
        get { return missionPillarObserver; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        Init();
        IsStop = false;
    }

    private void Init()
    {
        chestObjects = GameObject.FindGameObjectsWithTag("Chest");
        int pillarCnt = GameObject.FindGameObjectsWithTag("Pillar").Length;
        missionPillarObserver = new MissionPillarObserver(pillarCnt);
        missionCheck = new bool[(int)ENUM.MISSION_INDEX.MAXMISSION];
        int randomChest = Random.Range(0, chestObjects.Length);
        chestObjects[randomChest].GetComponent<MissionChest>().IsContained = true;
        maxCrystalCnt = GameObject.FindGameObjectsWithTag("Crystal").Length;
        crystalCnt = 0;
    }

    void Start()
    {
        StartCoroutine(CheckMission());
    }

    public void CompleteMission(ENUM.MISSION_INDEX mIndex)
    {
        missionCheck[(int)mIndex] = true;
    }

    //  크리스탈의 개수를 증가, 모두 수집하면 임무 완료
    public void IncreseCrystalCnt()
    {
        crystalCnt++;
        if (crystalCnt == maxCrystalCnt)
            CompleteMission(ENUM.MISSION_INDEX.CRYSTAL);
    }

    //  완료되지 않은 미션의 index를 반환, 없으면 -1
    public int GetNotCompletedMission()
    {
        for (int i = 0; i <= missionCheck.Length; i++)
        {
            if (!missionCheck[i]) return i;
        }
        return -1;
    }

    //  게임 시작시 완료된 미션이 있는지 체크하기 위한 코루틴
    private IEnumerator CheckMission()
    {
        StartCoroutine(CheckMissionPillar());
        while(GetNotCompletedMission() != -1)
        {
            yield return new WaitForSeconds(1.0f);
        }
        UIManager.instance.FinishUISetActive();
        StartCoroutine(ClickWait());
        yield break;
    }

    //  기둥이 모두 불이 켜졌는지 확인하기 위한 코루틴
    private IEnumerator CheckMissionPillar()
    {
        while (!missionPillarObserver.IsAllLighted())
        {
            yield return new WaitForSeconds(1.0f);
        }
        CompleteMission(ENUM.MISSION_INDEX.PILLAR);
        yield break;
    }

    //  미션을 모두 완료했을 때 완료UI가 표시되고, 마우스 클릭이 있을때 까지 대기하는 코루틴
    private IEnumerator ClickWait()
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        UIManager.instance.BackIntro();
    }
}

namespace ENUM
{
    public enum MISSION_INDEX
    {
        CRYSTAL,
        CHEST,
        PILLAR,
        ALTAR,

        MAXMISSION
    }
}
