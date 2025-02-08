using Army.Units;
using UnityEngine;

namespace Army.PlayerArmy {
    public class PlayerArmyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedForLateralMovement;
        [SerializeField] private Joystick _joystick;

        [SerializeField] private float _stopDistance;
        [SerializeField] private Transform _stopPoint;

        private bool _isMovingForward = true;
        private bool _isMoving = true;

        private void Update() {
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

            FindEnemyArmy();
        }

        private void Move(float speed)
        {
            var moveHorizontal = _joystick.Horizontal;

            var movement = new Vector3(moveHorizontal, 0f, speed * Time.deltaTime);
            transform.position = transform.position + movement * _speedForLateralMovement * Time.deltaTime;
        }

        private void FindEnemyArmy()
        {
            Ray ray = new Ray(_stopPoint.position, _stopPoint.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _stopDistance))
            {
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.TryGetComponent(out ArmyManager armyManager))
                    {
                        if (armyManager.GetComponentInChildren<Unit>().UnitType == UnitTypes.Enemy)
                        {
                            TrafficStop();
                        }                  
                    }
                }               
            }
            else
            {
                TrafficStart();
            }

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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_stopPoint.position, _stopPoint.forward * _stopDistance);
        }
    }
}