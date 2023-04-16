using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        [SerializeField] private float adjValue;
        private Animator animator;
        private BoxCollider2D boxCollider2D;
        private Rigidbody2D rigidBody2D;
        private Vector2 adjustedPos;
        private Vector3 exDir;
        private Vector3 lineStart;
        private bool isAxisX;
        private bool isAxisPosY;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            rigidBody2D = GetComponent<Rigidbody2D>();

            isAxisX = false;
            isAxisPosY = false;
        }

        private void Update()
        {
            if (GameManager.instance.IsStop) return;
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                isAxisX = true;
                isAxisPosY = false;
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                isAxisX = true;
                isAxisPosY = false;
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                isAxisX = false;
                isAxisPosY = true;
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                isAxisX = false;
                isAxisPosY = false;
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);
            rigidBody2D.velocity = speed * dir;

            //===============================================================
            adjustedPos = Vector2.zero;            
            if (dir != Vector2.zero) exDir = dir;
            if (isAxisX) adjustedPos = new Vector2(exDir.x * (boxCollider2D.size.x + adjValue), boxCollider2D.size.y) * 0.5f;
            if (isAxisPosY) adjustedPos = new Vector2(0.0f, exDir.y * boxCollider2D.size.y * 1.1f);
            lineStart = adjustedPos;
            lineStart += transform.position;
            //  플레이어의 위치에서 바라보는 방향으로 1.0f 만큼 레이캐스트
            RaycastHit2D hit = Physics2D.Linecast(lineStart, lineStart + exDir);
            //  키보드 입력 E를 누를 때
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E누름");
                if (hit.transform != null)
                {
                    Debug.Log(hit.transform.name);
                    Interact(hit.transform);
                }
            }
            Debug.DrawLine(lineStart, lineStart + exDir);
        }

        private void Interact(Transform targetTransform)
        {
            string tag = targetTransform.tag;           
            switch (tag)
            {
                case "Pillar":
                    targetTransform.GetComponent<MissionPillarSubject>().Interaction();
                    break;

                case "Chest":
                    targetTransform.GetComponent<MissionChest>().Interaction();
                    break;

                case "Statue":
                    targetTransform.GetComponent<Statue>().Interaction();
                    break;
            }
        }
    }
}
