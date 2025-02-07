using UnityEngine;

namespace Army.PlayerArmy {
    public class PlayerArmyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedForLateralMovement;
        [SerializeField] private Joystick _joystick;

        private bool _isMovingForward = true;
        private bool _isMoving = true;

        private void FixedUpdate() {
            if (_isMoving)
            {
                if (_isMovingForward)
                {
                    Move(_speed);
                }
                else
                {
                    Move(-_speed + 4);
                }
            }
            else
            {
                Move(0);
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
            _isMovingForward = false;
        }

        public void StartMovingForward()
        {
            _isMovingForward = true;
        }

        public void TrafficStop()
        {
            _isMoving = false;
        }

        public void TrafficStart()
        {
            _isMoving = true;
        }
        
    }
}