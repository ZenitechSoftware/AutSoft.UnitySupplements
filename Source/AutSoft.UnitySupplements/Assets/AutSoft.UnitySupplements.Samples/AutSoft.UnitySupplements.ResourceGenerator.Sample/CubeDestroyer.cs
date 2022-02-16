using System.Collections;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Sample
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class CubeDestroyer : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            StartCoroutine(DestroyAfterTime());
        }

        private void FixedUpdate() => _rigidbody.AddForce(Physics.gravity);

        private IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(5.0f);

            Destroy(gameObject);
        }
    }
}