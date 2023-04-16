using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPillarSubject : InteractableObject, ISubject
{
    //private Cainos.PixelArtTopDown_Basic.SpriteColorAnimation scAnimation;
    private GameObject glowObject;
    private SpriteRenderer sr;
    private List<IObserver> observers;

    private void Awake()
    {
        observers = new List<IObserver>();
        glowObject = transform.GetChild(0).gameObject;
        sr = GetComponent<SpriteRenderer>();

        SpriteRenderer[] childSpriteRenders = GetComponentsInChildren<SpriteRenderer>();
        foreach(var r in childSpriteRenders)
        {
            r.sortingLayerName = sr.sortingLayerName;
            r.sortingOrder = sr.sortingOrder;
        }
    }

    private void Start()
    {
        glowObject.SetActive(false);
        observers.Add(GameManager.instance.propMissionPillarObserver);
    }

    public void RegisterObserver(ref IObserver _observer)
    {
        this.observers.Add(_observer);
    }

    public void RemoveObserver(ref IObserver _observer)
    {
        this.observers.Remove(_observer);
    }

    public void NotifyObservers()
    {
        foreach(var observer in observers)
        {
            observer.Update();
        }
    }

    //  �÷��̾ ��ȣ�ۿ�� ����Ʈ�� ����� ���������� ������ �ϴ� �Լ�
    public override void Interaction()
    {
        if (glowObject.activeSelf) return;
        Debug.Log("Interaction ����");
        NotifyObservers();
        glowObject.SetActive(true);
    }
}
