using UnityEngine;

namespace Army.PlayerArmy {
    public class PlayerArmyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedForLateralMovement;
        [SerializeField] private Joystick _joystick;

        private bool isMovingForward = true;
        private bool isMoving = true;

        private void FixedUpdate() {
            if (isMoving)
            {
                if (isMovingForward)
                {
                    Move(_speed);
                }
                else
                {
                    Move(-_speed + 4);
                }
            }

        }

        private void Move(float speed)
        {
            var moveHorizontal = _joystick.Horizontal;

            var movement = new Vector3(moveHorizontal, 0f, speed * Time.deltaTime);
            transform.position = transform.position + movement * _speedForLateralMovement * Time.deltaTime;
        }



        public void StartMovingBack()
        {
            isMovingForward = false;
        }

        public void StartMovingForward()
        {
            isMovingForward = true;
        }

        public void TrafficStop()
        {
            isMoving = false;
        }
        
    }
}