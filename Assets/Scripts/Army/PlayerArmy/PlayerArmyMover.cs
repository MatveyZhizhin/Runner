using UnityEngine;

namespace Army.PlayerArmy {
    public class PlayerArmyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _speedForLateralMovement;
        [SerializeField] private Joystick _joystick;

        private void FixedUpdate() {
            Move();
        }

        private void Move()
        {
            var moveHorizontal = _joystick.Horizontal;

            var movement = new Vector3(moveHorizontal, 0f, _speed * Time.deltaTime);
            transform.position = transform.position + movement * _speedForLateralMovement * Time.deltaTime;
        }
    }
}