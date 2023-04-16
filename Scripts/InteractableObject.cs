using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  상호작용이 필요한 오브젝트에 상속하기 위한 추상 함수
public abstract class InteractableObject : MonoBehaviour
{

    //  상호 작용이 가능한 오브젝트가 상호 작용 수행시 작동
    public virtual void Interaction() { }
}
