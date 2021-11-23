using System.Collections;
using UnityEngine;

namespace Characters.MovingController
{
    public class PlayerMovingController : MonoBehaviour
    {
        private CharacterController characterController;
        private Animator characterAnimator;
        private Vector3 movementDirection;
        private Transform characterTransform;
        private float velocity;
        private Vector3 _impact = Vector3.zero;

        private Animator animator;


        private bool isCrouched;
        private float originHeight;

        public float SprintingSpeed;
        public float WalkSpeed;

        public float SprintingSpeedWhenCrouched;
        public float WalkSpeedWhenCrouched;

        public float Gravity = 9.8f;
        public float JumpHeight;
        public float CrouchHeight = 1f;

        public float mass;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            characterAnimator = GetComponentInChildren<Animator>();
            characterTransform = transform;
            originHeight = characterController.height;
            animator=gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateImpact();
            float tmp_CurrentSpeed = WalkSpeed;
            if (characterController.isGrounded)
            {
                var tmp_Horizontal = Input.GetAxis("Horizontal");
                var tmp_Vertical = Input.GetAxis("Vertical");
                movementDirection =
                    characterTransform.TransformDirection(new Vector3(tmp_Horizontal, 0, tmp_Vertical));

                animator.SetBool("isMovingF",tmp_Vertical>0);
                animator.SetBool("isMovingB",tmp_Vertical<0);
                animator.SetBool("isMovingR",tmp_Horizontal>0);
                animator.SetBool("isMovingL",tmp_Horizontal<0);
                animator.SetBool("isMoving", tmp_Horizontal!=0||tmp_Vertical!=0);



                if (Input.GetButtonDown("Jump"))
                {
                    movementDirection.y = JumpHeight;
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    var tmp_CurrentHeight = isCrouched ? originHeight : CrouchHeight;
                    StartCoroutine(DoCrouch(tmp_CurrentHeight));
                    isCrouched = !isCrouched;
                }

                if (isCrouched)
                {
                    tmp_CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeedWhenCrouched : WalkSpeedWhenCrouched;
                }
                else
                {
                    tmp_CurrentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintingSpeed : WalkSpeed;
                }

                var tmp_Velocity = characterController.velocity;
                tmp_Velocity.y = 0;
                velocity = tmp_Velocity.magnitude;
                // characterAnimator.SetFloat("Velocity", velocity, 0.25f, Time.deltaTime);
            }

            movementDirection.y -= Gravity * Time.deltaTime;
            characterController.Move(tmp_CurrentSpeed * Time.deltaTime * movementDirection);
        }

        private IEnumerator DoCrouch(float _target)
        {
            float tmp_CurrentHeight = 0;
            while (Mathf.Abs(characterController.height - _target) > 0.1f)
            {
                yield return null;
                characterController.height =
                    Mathf.SmoothDamp(characterController.height, _target,
                        ref tmp_CurrentHeight, Time.deltaTime * 5);
            }
        }
        
        // added
        public void AddImpact(Vector3 dir, float force){
            dir.Normalize();
            if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
            _impact += dir.normalized * force / mass;
        }

        private void UpdateImpact()
        {
            // apply the impact force:
            if (_impact.magnitude > 0.2) characterController.Move(_impact * Time.deltaTime);
            // consumes the impact energy each cycle:
            _impact = Vector3.Lerp(_impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }
}
