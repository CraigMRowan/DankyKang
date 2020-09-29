using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrel = null;
    [SerializeField] private float spawnTime = 0f;
    [SerializeField] private float spawnDelay = 0f;
    [SerializeField] private float barrelForce = 0f;

    private bool _stopSpawning = false;
    private GameObject _barrelParent = null;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnBarrel), spawnTime, spawnDelay);
        _barrelParent = new GameObject("Barrels");
    }

    private void SpawnBarrel()
    {
        var transform1 = transform;

        var cloneBarrel = Instantiate(barrel, transform1.position, transform1.rotation, _barrelParent.transform);
        var localRigidbody2D = cloneBarrel.GetComponent<Rigidbody2D>();
        localRigidbody2D.velocity = Vector3.right * barrelForce;

        if (_stopSpawning)
            CancelInvoke(nameof(SpawnBarrel));
    }
}
