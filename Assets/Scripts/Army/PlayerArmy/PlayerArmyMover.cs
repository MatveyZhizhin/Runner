using Army.Units;
using System.Collections;
using UnityEngine;
using AnimatorsConstans;

namespace Army.PlayerArmy {
    public class PlayerArmyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedForLateralMovement;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private float _pushSpeed;
        [SerializeField] private float _pushRate;

        [SerializeField] private float _stopDistance;
        [SerializeField] private Transform _stopPoint;
        [SerializeField] private LayerMask _obstacles;

        private bool _isMoving = true;
        private bool _isMovingForward = true;

        private ArmyManager _armyManager;

        private void Awake()
        {
            TryGetComponent(out _armyManager);
        }

        private void Update() {
            if (_isMoving)
            {
                if (_isMovingForward)
                {
                    Move(_speed);
                    ChangeUnitsState(true);
                }
                else
                {
                    Move(-_speed - _pushSpeed);
                    ChangeUnitsState(false);
                }
            }
            else
            {
                Move(0);
                ChangeUnitsState(false);
            }

            FindEnemyArmy();
        }

        private void Move(float speed)
        {
            var moveHorizontal = _joystick.Horizontal;

            var movement = new Vector3(speed, 0f, -moveHorizontal);
            transform.Translate(movement * _speedForLateralMovement * Time.deltaTime);
        }

        private void FindEnemyArmy()
        {
            Ray ray = new Ray(_stopPoint.position, _stopPoint.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, _stopDistance, _obstacles))
            {
                if (hitInfo.collider != null)
                {                                            
                   TrafficStop();                                       
                }               
            }
            else
            {
                TrafficStart();
            }
        }

        private void ChangeUnitsState(bool isRunning)
        {
            foreach (var unit in _armyManager.GetUnits())
            {
                unit.GetComponent<Animator>().SetBool(CharacterAnimationConstans.IsRunning, isRunning);
            }
        }

        public IEnumerator Pushing()
        {
            _isMovingForward = false;
            yield return new WaitForSeconds(_pushRate);
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