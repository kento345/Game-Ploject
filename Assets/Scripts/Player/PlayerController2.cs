using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{

    [Header("移動設定")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    public bool isGround2 = false;

    [Header("当たり判定")]
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0f, -0.9f, 0f);
    [SerializeField] private float groundCheckRadius = 0.3f;

    //-----その他-----
    private Rigidbody rb;

    private void Start()
    {
       
        isGround2 = false;
        rb= GetComponent<Rigidbody>();
    }
    //-----移動入力-----
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    //-----ジャンプ入力-----
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround2 && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (!enabled)
        {
            isGround2 = false;
        }
        if (rb != null)
        {
            //-----移動-----
            Vector3 move = new Vector3(inputMove.x, 0f, inputMove.y) * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            //-----ジャンプ(地面の当たり判定)-----
            Vector3 checkPosition = transform.position + groundCheckOffset;
            isGround2 = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGround2 ? Color.green : Color.red;
        Vector3 checkPosition = transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, Vector3.one * groundCheckRadius * 2f);
    }
}
