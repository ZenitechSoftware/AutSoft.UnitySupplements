#nullable enable
using System.Collections;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples.ResourceGeneratorSamples
{
    public sealed class SpawnCubes : MonoBehaviour
    {
        [SerializeField] private Transform _target = default!;

        private void Start() => StartCoroutine(SpawnCubesContinuously());

        private IEnumerator SpawnCubesContinuously()
        {
            var prefab = Resources.Load<GameObject>(ResourcePaths.Prefabs.Cube);

            while (gameObject)
            {
                Instantiate(prefab, _target.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
