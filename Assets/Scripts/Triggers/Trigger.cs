using UnityEngine;

namespace Assets.Scripts.Triggers
{
    [RequireComponent(typeof(Collider))]
    public class Trigger<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected virtual void OnEnter(T triggered) { }

        protected virtual void OnExit(T triggered) { }

        protected virtual void OnStay(T triggered) { }


        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                if (other.TryGetComponent(out T triggered))
                {
                    OnEnter(triggered);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other != null)
            {
                if (other.TryGetComponent(out T triggered))
                {
                    OnStay(triggered);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other != null)
            {
                if (other.TryGetComponent(out T triggered))
                {
                    OnExit(triggered);
                }
            }
        }
    }
}
