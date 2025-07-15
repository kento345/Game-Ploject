using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField]private float speed_ = 5f;  //移動速度
    private Vector2 inputMove;                  //入力された移動方向 
    public void OnMove(InputAction.CallbackContext context)
    {
        //入力値を読み取る
        inputMove = context.ReadValue<Vector2>();  
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        //Vector2からVector3に変更
        Vector3 move = new Vector3 (inputMove.x, 0f ,inputMove.y) * speed_ * Time.deltaTime;
        //現在位置に加算して移動
        transform.position += move;
    }
}
