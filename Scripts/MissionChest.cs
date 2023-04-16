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

    //  ���ڰ� ���ȴ��� ���ο� ������ ����ִ��� ������ ����� ��ȯ�ϴ� �Լ�
    public bool IsChestOpened()
    {
        //  ���ڰ� ���������� false�� ��ȯ
        if (isOpened) return false;

        //  ���ڰ� ������������ ����� ����
        //  ������ ����ִ� ���ڴ� isContained�� true �� ��ȯ �� �̼� Ŭ����
        //  �������� isContained�� false�� ��ȯ�ϱ� ������ �ٸ� ���ڸ� ������� �Ѵ�.
        spriteRenderer.sprite = openedChest;
        isOpened = true;
        return IsContained;
    }

    //  �÷��̾ ���ڸ� ������ ��, ������ ��������� �ӹ��� �ϼ�
    public override void Interaction()
    {
        if (IsChestOpened()) GameManager.instance.CompleteMission(ENUM.MISSION_INDEX.CHEST);
    }
}
