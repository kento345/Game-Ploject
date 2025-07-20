using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField,Header("移動速度")]
    private float speed_ = 5f; 
    private Vector2 inputMove;                  //入力された移動方向 
    //-----------ジャンプ------------
    [SerializeField,Header("ジャンプ力")] 
    private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGround = true;
    [SerializeField] 
    private LayerMask groundLayer;
    //--------------Scriptチェンジ--------------    
    private PlayerController myScript;
    private PlayerController myScript2;
    [SerializeField] private GameObject player2;
    [SerializeField] private bool isJump;

    public void OnMove(InputAction.CallbackContext context)
    {
        //入力値を読み取る
        inputMove = context.ReadValue<Vector2>();  
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround && isJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }
    public void OnChar1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            myScript.enabled = false;
            isJump = false;
        }
    }
    public void OnChar2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            myScript.enabled = true;
            isJump = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myScript = GetComponent<PlayerController>();
        myScript2 = player2.GetComponent<PlayerController>();
        myScript2.enabled = false;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
           isGround = true;
        }
    }
}
