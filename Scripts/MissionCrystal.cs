using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCrystal : InteractableObject
{

    private Vector3 highPos;
    private Vector3 lowPos;
    private bool isUp;
    private float unit;
    private float time;


    private void Awake()
    {
        time = 0.0f;
        unit = 0.05f;
        lowPos = this.transform.position;
        highPos = lowPos + new Vector3(0, unit * 10, 0);
        int randomValue = Random.Range(1, 6);
        Vector3 adjustedPos = new Vector3(0, unit * randomValue, 0);
        randomValue = Random.Range(0, 2);
        isUp = randomValue == 1 ? true : false;
        this.transform.position += adjustedPos;
    }

    //  1�ʸ��� ���� ������ �ٲٸ� ���ִ� ���� ȿ���� �ֱ� ���� Lerp�� �̿��Ͽ� ũ����Ż�� ��ġ�� �ٲ�
    private void Update()
    {
        time += Time.deltaTime;
        this.transform.position = Vector3.Lerp(this.transform.position, isUp ? highPos : lowPos, Time.deltaTime);
        if (time >= 1.0f)
        {
            time = 0.0f;
            isUp = !isUp;
        }
    }

    //  �÷��̾� ĳ������ Ʈ���Ű� �����ҽ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        Interaction();
    }

    //  ũ����Ż�� ������ ������Ű�� ������Ʈ�� �ı�
    public override void Interaction()
    {
        GameManager.instance.IncreseCrystalCnt();
        Destroy(this.gameObject);
    }
}
