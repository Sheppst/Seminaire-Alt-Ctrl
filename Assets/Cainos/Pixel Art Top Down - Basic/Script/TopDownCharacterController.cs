using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        bool StopMovement;
        private Animator animator;
        public Vector2 dir = Vector2.zero;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            //if (!(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))) 
            //{
            //    StopMovement = false;
            //}
            //else
            //{
            //    StopMovement = true;
            //    GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            //}
            
            //if (!StopMovement)
            //{
            //
            //}
                dir = Vector2.zero;
                if (Input.GetKey(KeyCode.A))
                {
                    dir.x = -1;
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    dir.x = 1;
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    dir.y = 1;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    dir.y = -1;
                }
                dir.Normalize();
                animator.SetBool("IsMoving", dir.magnitude > 0);
                GetComponent<Rigidbody2D>().linearVelocity = speed * dir;


            print(GetComponent<Rigidbody2D>().linearVelocity);


            if (dir == Vector2.zero)
            {
                GetComponent<Animator>().SetInteger("AnimState", 0);
            }
            else
            {
                GetComponent<Animator>().SetInteger("AnimState", 1);
            }

            if (Input.GetMouseButtonDown(1))
            {
                //StopMovement = true;
                dir = Vector2.zero;
                GetComponent<Animator>().SetTrigger("Attack2");
            }


            if (Input.GetMouseButtonDown(2))
            {
                //StopMovement = true;
                dir = Vector2.zero;
                GetComponent<Animator>().SetTrigger("Attack3");
            }

            if (Input.GetMouseButtonDown(0))
            {
                //StopMovement = true;
                dir = Vector2.zero;
                GetComponent<Animator>().SetTrigger("Attack1");
            }


        }
        //private void FixedUpdate()
        //{
        //}
    }
}
