using UnityEditor.Profiling;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //-----à⁄ìÆ-----
    private Vector3 pos;
    private Vector3 num;
    [SerializeField] private float speed = 3f;

    bool isRotation = false;

    Rigidbody rb;

    [SerializeField] private Data data;     //ìGID
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        num.x = 1;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        pos = transform.forward;

        Ray ray;
        RaycastHit hit;

        var origin = transform.position;
        var direction = transform.forward;

        ray = new Ray(origin, direction);
        Debug.DrawRay(origin, direction * 2f, Color.red);

        if (Physics.Raycast(ray, out hit, 2f))
        {
            isRotation = true;
            Debug.Log("à⁄ìÆí‚é~");
            rb.linearVelocity = Vector3.zero;
            /* Quaternion rot = Quaternion.identity;
             rot = Quaternion.Slerp(rb.rotation,rot,10.0f * Time.deltaTime);
             transform.rotation = rot;*/
        }
        isRotation = false;

        if (isRotation)
        {
           
        }

        if (!isRotation)
        {
            Debug.Log("à⁄ìÆäJén");
            rb.linearVelocity = pos * speed;
        }
    }


    //-----IDï Ç…èàóù-----
    public void SelectID()
    {
        //IDÇéÊìæ
        int enemyID = data.GetID();

        switch (enemyID)
        {
            //ID1Ç»ÇÁ
            case 1:
                Debug.Log("ID1");
                break;
            //ID2Ç»ÇÁ
            case 2:
                Debug.Log("ID2");
                break;
            default:
                Debug.Log("ÇªÇÃëº");
                break;
        }
    }
}
