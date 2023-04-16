using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionChest : InteractableObject
{
    [SerializeField] private Sprite openedChest;

    private SpriteRenderer spriteRenderer;
    private bool isOpened;
    private bool isContained;

    
    public bool IsContained
    {
        get { return isContained; }
        set { isContained = value; }
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isOpened = false;
        IsContained = false;
    }

    //  상자가 열렸는지 여부와 물건이 담겨있는지 여부의 결과를 반환하는 함수
    public bool IsChestOpened()
    {
        //  상자가 열려있으면 false를 반환
        if (isOpened) return false;

        //  상자가 닫혀있을때만 여기로 진입
        //  물건이 담겨있는 상자는 isContained가 true 를 반환 후 미션 클리어
        //  나머지는 isContained가 false를 반환하기 때문에 다른 상자를 열어봐야 한다.
        spriteRenderer.sprite = openedChest;
        isOpened = true;
        return IsContained;
    }

    //  플레이어가 상자를 열었을 때, 물건이 들어있으면 임무를 완수
    public override void Interaction()
    {
        if (IsChestOpened()) GameManager.instance.CompleteMission(ENUM.MISSION_INDEX.CHEST);
    }
}
