using UnityEditor.Profiling;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //-----ˆÚ“®-----
    private Vector3 pos;
    private int num = 1;
    [SerializeField] private float speed = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        pos = transform.position;
        transform.Translate(transform.right * Time.deltaTime * speed * num);

        if (pos.x >11)
        {
            num = -1;
        }
        else if (pos.x < -7.5)
        {
            num = 1;
        }
    }
}
