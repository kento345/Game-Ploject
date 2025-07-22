using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField, Header("移動速度")]
    private float speed_ = 5f;
    private Vector2 inputMove;                  //入力された移動方向 
    //-----------ジャンプ------------
    [SerializeField, Header("ジャンプ力")]
    private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGround = true;
    [SerializeField]
    private LayerMask groundLayer;
    //--------------Scriptチェンジ--------------    
    private PlayerController1 myScript1;
    [SerializeField] private bool isJump2;
    private PlayerController2 myScript2;
    private bool isJump1;

    public void OnMove(InputAction.CallbackContext context)
    {
        //入力値を読み取る
        inputMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround && isJump2)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }
    public void OnChar2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            myScript1.enabled =false;
            myScript2.enabled = true;
            isJump2 = true;
            isJump1 = false;
        }
    }

    private void Awake()
    {
        myScript2.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        myScript1 = GetComponent<PlayerController1>();
        myScript2 = GetComponent<PlayerController2>();
        isJump1   = GetComponent<PlayerController1>();
    }

    // Update is called once per frame
    void Update()
    {
        Move2();
    }
    void Move2()
    {
        //Vector2からVector3に変更
        Vector3 move = new Vector3(inputMove.x, 0f, inputMove.y) * speed_ * Time.deltaTime;
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
