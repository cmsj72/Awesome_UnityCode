using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers

    //오브젝트가 트리거에 진입할때, 할당된 레이어와 소팅 레이어를 변경
    //플레이어가 레이어 사이를 왔다갔다 하기 위한 계단 오브젝트에 사용됨
    public class LayerTrigger : MonoBehaviour
    {
        public string layer;
        public string sortingLayer;

        private void OnTriggerExit2D(Collider2D other)
        {
            //  검출된 오브젝트의 레이어를 할당된 레이어 값으로 변경
            other.gameObject.layer = LayerMask.NameToLayer(layer);

            //  스프라이트 렌더러 컴포넌트의 소팅 레이어 이름을 할당된 소팅 레이어 값으로 변경
            other.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayer;
            //  검출된 오브젝트의 자식들에서 스프라이트 렌더러 컴포넌트들을 받아옴
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            //  스프라이트 렌더러들의 레이어 이름을 소팅 레이어로 변경
            foreach ( SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }
        }

    }
}
