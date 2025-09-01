using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShotManager : MonoBehaviour
{
    [Header("発射場所")]
    [SerializeField] private GameObject firPoint;

    [Header("弾のオブジェクト")]
    [SerializeField] private GameObject bullet1;
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
    private RaycastHit hit;                               //Raycastの情報

   
    void Start()
    {
       
    }

    private void Update()
    {
        Aim();
    }

    //-----クロスヘアー-----
    void Aim()
    {
        //Rayを生成
        ray = new Ray(origin.transform.position, Camera.main.transform.forward);
        //Rayの視覚化
        Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);

        if (Physics.Raycast(ray, out hit, 30.0f))
        {
            int hitLayer = hit.collider.gameObject.layer;

            if (hitLayer == LayerMask.NameToLayer("Enemy"))
            {
                crosshair.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                crosshair.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
            }
        }
        else
        {
            crosshair.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    void Shot()
    {
        //Rayに当たったObjを消す
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject != null)
            {
                //当たったObjのScriptを取得
                EnemyBase enemy = hit.collider.gameObject.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    Destroy(hit.collider.gameObject);
                    //enemyのSelectIDを実行
                    enemy.SelectID();
                }
            }
        }
    }

    public void OnShot1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 bulletPosition = firPoint.transform.position;
            GameObject newBullet = Instantiate(bullet1, bulletPosition, this.gameObject.transform.rotation);
            Vector3 direction = Camera.main.transform.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            newBullet.name = bullet1.name;
            Destroy(newBullet, 0.8f);
        }
    }

    public void OnShot2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(efectInstance == null)
            {
                Shot();
                efectInstance = Instantiate(efect, firPoint.transform.position, Camera.main.transform.rotation);
                ParticleSystem ps = efectInstance.GetComponent<ParticleSystem>();
            }
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
}
