using UnityEngine;

public class BarrelDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrel"))
        {
            Destroy(collision.gameObject);
        }
    }
}
