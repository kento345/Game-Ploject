using UnityEngine;

public class BulletManeger : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}