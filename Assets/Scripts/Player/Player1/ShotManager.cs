using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShotManager : MonoBehaviour
{
    [Header("発射場所")]
    [SerializeField] private GameObject firPoint;

    private GameObject enemy;
    private GameObject player;
    private float speed = 50f;
  

    [Header("クロスヘアー"), SerializeField]
    private Image crosshair;
    [Header("Ray生成Obj"), SerializeField]
    private GameObject origin;
    [Header("エフェクト"), SerializeField]
    private GameObject efect;
    private GameObject efectInstance;

    [Header("Ray")]
    private Ray ray;
    private RaycastHit hit;

    [SerializeField] private LayerMask enemyLayer;//Layerの情報
    [SerializeField] private LayerMask player2Layer;//Layerの情報

   
    void Start()
    {
       
    }

    private void Update()
    {
        Aim();

        if (efectInstance != null)
        {
            efectInstance.transform.position = firPoint.transform.position;
            efectInstance.transform.rotation = Camera.main.transform.rotation;
        }
    }

    //-----クロスヘアー-----
    void Aim()
    {
        int IgnoreLayer = LayerMask.NameToLayer("Player1");
        int layerMask = ~(1 << IgnoreLayer);
        //Rayを生成
        ray = new Ray(origin.transform.position, Camera.main.transform.forward);
        //Rayの視覚化
        Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);
        //Debug.Log(hit.collider.gameObject.name);

        if (Physics.Raycast(ray, out hit, 30.0f,layerMask))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (((1 << hitLayer) & enemyLayer) != 0)
            {
                crosshair.color = Color.red;
            }
            else if (((1 << hitLayer) & player2Layer) != 0)
            {
                crosshair.color = Color.yellow;
            }
            else
            {
                crosshair.color = Color.cyan;
            }
        }
        else
        {
            crosshair.color = Color.cyan;
        }
    }

    void Shot()
    {
        int IgnoreLayer = LayerMask.NameToLayer("Player1");
        int layerMask = ~(1 << IgnoreLayer);
        ray = new Ray(origin.transform.position, Camera.main.transform.forward);
        //Rayに当たったObjを消す
        if (Physics.Raycast(ray, out hit,30.0f,layerMask))
        {
            if (enemy != null || player != null) { return; }
            GameObject hitObj = hit.collider.gameObject;
            if (((1 << hitObj.layer) & enemyLayer) != 0)
            {
                //hitObj.transform.position = Vector3.zero;
                enemy = Instantiate(hitObj);
                enemy.SetActive(false);

                Destroy(hitObj, 0.5f);
                //当たったObjのScriptを取得
                EnemyBase enemyBase = hit.collider.gameObject.GetComponent<EnemyBase>();
                if (enemyBase != null)
                {
                    Destroy(hit.collider.gameObject, 0.5f);
                    //enemyのSelectIDを実行
                    enemyBase.SelectID();
                }
            }
            if(((1 << hitObj.layer) & player2Layer) != 0)
            {
                Debug.Log("PlayerHit");
                player = hitObj;
                player.SetActive(false);
            }
        }
    }

    public void OnShot1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (efectInstance == null)
            {
                efectInstance = Instantiate(efect, firPoint.transform.position, Camera.main.transform.rotation);
                //ParticleSystem ps = efectInstance.GetComponent<ParticleSystem>();
            }
            Shot();
        }
        if (context.canceled)
        {
            if (efectInstance != null)
            {
                Destroy(efectInstance);
                efectInstance = null;
            }
        }
    }

    public void OnShot2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            //if(enemy == null || player == null) { return; }
            if (enemy != null)
            {
                //弾の発射(不必要)
                Vector3 bulletPosition = firPoint.transform.position;
                GameObject newBullet = Instantiate(enemy, bulletPosition, this.gameObject.transform.rotation);
                Vector3 direction = Camera.main.transform.forward;
                newBullet.SetActive(true);
                /* EnemyBase enemyBase = newBullet.GetComponent<EnemyBase>();
                 enemyBase.enabled = false;*/
                Rigidbody rb = newBullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direction * speed, ForceMode.Impulse);
                }
                Destroy(enemy);
                enemy = null;
                //newBullet.name = enemy.name;
            }
            else if (player != null)
            {
                Vector3 dir = Camera.main.transform.forward;
                Vector3 playerPoint = transform.position + dir * 5.0f;
                player.transform.position = playerPoint;
                player.SetActive(true);

                player = null;
            }
        }


    }
}
