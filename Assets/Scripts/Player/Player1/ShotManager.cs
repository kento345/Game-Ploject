using UnityEngine;
using UnityEngine.InputSystem;

public class ShotManager : MonoBehaviour
{
    [Header("発射場所")]
    [SerializeField] private GameObject firPoint;

    [Header("弾のオブジェクト")]
    [SerializeField] private GameObject bullet1;
    //[SerializeField] private GameObject bullet2;

    private float speed = 30f;
    void Start()
    {
        
    }

    public void OnShot1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector3 bulletPosition = firPoint.transform.position;
            GameObject newBullet = Instantiate(bullet1, bulletPosition, this.gameObject.transform.rotation);
            Vector3 direction = -newBullet.transform.right;
            newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            newBullet.name = bullet1.name;
            //Destroy(newBullet,0.8f);
        }
    }

    void OnShot2(InputAction.CallbackContext context)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
