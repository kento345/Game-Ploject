using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    public bool isGround = false;

    [Header("当たり判定")]
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0f, -0.9f, 0f);
    [SerializeField] private float groundCheckRadius = 0.3f;

   /* [Header("プレイヤーオブジェクト")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;*/

    [Header("カメラ切り替え")]


    //-----その他-----
    private GameObject activePlayer;
    private Rigidbody activeRb;
     
     
    private void Awake()
    {
        //SetActivePlayer(player1);
        activeRb = activePlayer.GetComponent<Rigidbody>();
    }
    //-----移動入力-----
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    //-----ジャンプ入力-----
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround && activeRb != null)
        {
            activeRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    //-----Player1切り替え入力-----
    public void OnChar1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //SetActivePlayer(player1);
        }
    }
    //-----Player2切り替え入力-----
    public void OnChar2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //SetActivePlayer(player2);
        }
    }

    private void FixedUpdate()
    {
        if (activePlayer != null && activeRb != null)
        {
            //-----移動-----
            Vector3 move = new Vector3(inputMove.x, 0f, inputMove.y) * speed * Time.fixedDeltaTime;
            activeRb.MovePosition(activeRb.position + move);

            //-----ジャンプ(地面の当たり判定)-----
            Vector3 checkPosition = activePlayer.transform.position + groundCheckOffset;
            isGround = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);
        }
    }

    //-----キャラクター切り替え-----
   /* private void SetActivePlayer(GameObject playerObj)
    {
        // 両方のPlayerControllerを一度無効にする
        player1.GetComponent<PlayerController>().enabled = false;
        player2.GetComponent<PlayerController>().enabled = false;
        // 指定されたプレイヤーのPlayerControllerを有効にする
        playerObj.GetComponent<PlayerController>().enabled = true;
        activePlayer = playerObj;
        activeRb = activePlayer.GetComponent<Rigidbody>();

        isGround = false;
    }*/

    //-----Gizmosの描画-----
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGround ? Color.green : Color.red;
        Vector3 checkPosition = transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, Vector3.one * groundCheckRadius * 2f);
    }
}
