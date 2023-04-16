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

    //  ũ����Ż�� ������ ����, ��� �����ϸ� �ӹ� �Ϸ�
    public void IncreseCrystalCnt()
    {
        crystalCnt++;
        if (crystalCnt == maxCrystalCnt)
            CompleteMission(ENUM.MISSION_INDEX.CRYSTAL);
    }

    //  �Ϸ���� ���� �̼��� index�� ��ȯ, ������ -1
    public int GetNotCompletedMission()
    {
        for (int i = 0; i <= missionCheck.Length; i++)
        {
            if (!missionCheck[i]) return i;
        }
        return -1;
    }

    //  ���� ���۽� �Ϸ�� �̼��� �ִ��� üũ�ϱ� ���� �ڷ�ƾ
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

    //  ����� ��� ���� �������� Ȯ���ϱ� ���� �ڷ�ƾ
    private IEnumerator CheckMissionPillar()
    {
        while (!missionPillarObserver.IsAllLighted())
        {
            yield return new WaitForSeconds(1.0f);
        }
        CompleteMission(ENUM.MISSION_INDEX.PILLAR);
        yield break;
    }

    //  �̼��� ��� �Ϸ����� �� �Ϸ�UI�� ǥ�õǰ�, ���콺 Ŭ���� ������ ���� ����ϴ� �ڷ�ƾ
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
