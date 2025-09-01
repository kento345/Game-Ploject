using UnityEditor.Profiling;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //-----ˆÚ“®-----
    private Vector3 pos;
    private int num = 1;
    [SerializeField] private float speed = 3f;

    [SerializeField] private Data data;     //“GID
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

    //-----ID•Ê‚Éˆ—-----
    public void SelectID()
    {
        //ID‚ðŽæ“¾
        int enemyID = data.GetID();

        switch (enemyID)
        {
            //ID1‚È‚ç
            case 1:
                Debug.Log("ID1");
                break;
            //ID2‚È‚ç
            case 2:
                Debug.Log("ID2");
                break;
            default:
                Debug.Log("‚»‚Ì‘¼");
                break;
        }
    }
}
