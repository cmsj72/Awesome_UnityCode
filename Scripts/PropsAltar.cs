using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{
    public class PropsAltar : InteractableObject
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;
        private bool isVisited;

        private void Awake()
        {
            isVisited = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //  알파 값을 1로 만들어서 색을 나타낸다
            targetColor = new Color(1, 1, 1, 1);
            if (!isVisited) Interaction();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //  알파 값을 0으로 만들어서 사라지게 만든다
            targetColor = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            //  현재 색에서 목표 색의 값까지 보간 속도와 델타타임을 이용해 변경
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            //  스프라이트렌더러를 저장한 리스트를 돌며 모두 색을 변경
            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }

        public override void Interaction()
        {
            isVisited = true;
            GameManager.instance.CompleteMission(ENUM.MISSION_INDEX.ALTAR);
        }
    }
}
