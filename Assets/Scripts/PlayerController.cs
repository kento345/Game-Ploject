using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed_ = 5f;
    private Vector2 inputMove;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGround = false;


    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0f, -0.9f, 0f);
    [SerializeField] private float groundCheckRadius = 0.3f;
    private Vector3 groundCheckPosition;


    [Header("プレイヤーオブジェクト")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private PlayerController myScript1;
    private PlayerController myScript2;

    private GameObject activePlayer;
    private Rigidbody activeRb;

  
    private void Awake()
    {
        activeRb = GetComponent<Rigidbody>();

        myScript1 = player1.GetComponent<PlayerController>();
        myScript2 = player2.GetComponent<PlayerController>();

        SetActivePlayer(player1, myScript1);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeRb = GetComponent<Rigidbody>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        //入力値を読み取る
        inputMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround && activeRb != null)
        {
            activeRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;

            //Gizomos使用
            /* activeRb.linearVelocity = new Vector3(activeRb.linearVelocity.x,0,activeRb.linearVelocity.z);
             activeRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
             isGround = false;*/
        }
    }
    public void OnChar1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetActivePlayer(player1, myScript1);
        }
    }
    public void OnChar2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetActivePlayer(player2, myScript2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

      /*  groundCheckPosition = transform.position + groundCheckOffset;
        isGround = Physics.CheckBox(groundCheckPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);*/
    }
    void Move()
    {
        if (activePlayer != null && activeRb != null)
        {
            Vector3 move = new Vector3(inputMove.x, 0f, inputMove.y) * speed_ * Time.deltaTime;
            activeRb.MovePosition(activeRb.position + move);
        }
    }
    void SetActivePlayer(GameObject playerObj, PlayerController script)
    {
        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = false;

        script.enabled = true;
        activePlayer = playerObj;
        activeRb = activePlayer.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGround = true;
        }
    }

    /*   private void OnDrawGizmosSelected()
       {
           Gizmos.color = isGround ? Color.green : Color.red;
           Vector3 checkPosition = transform.position + groundCheckOffset;
           Gizmos.DrawWireCube(checkPosition, Vector3.one * groundCheckRadius * 2f);
       }*/
}
