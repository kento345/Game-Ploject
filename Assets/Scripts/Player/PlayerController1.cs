using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float highjumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask player2Layer;
    public bool isGround1 = false;
    public bool isHighjump = false;

    [Header("当たり判定")]
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius;

    [Header("キャラクター補正")]
    [SerializeField] private Transform modelTransform;
    private float modelFacingOffsetY = 170f;

    //-----カメラ-----
    private Transform cameraTrans;

    //-----その他-----
    private Rigidbody rb;


    private void Start()
    {   
        //-----初期化-----
        isGround1 = false;
        isHighjump = false;
        cameraTrans = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }
    //-----移動入力-----
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    //-----ジャンプ入力-----
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround1 && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (context.performed && isHighjump && rb != null)
        {
            rb.AddForce(Vector3.up * highjumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
            Move();
            Jump();
    }

    void Move()
    {
        //-----カメラ回転-----
        Vector3 camFowerd = cameraTrans.forward;
        Vector3 camRight = cameraTrans.right;

        camFowerd.y = 0;
        camRight.y = 0;
        camFowerd.Normalize();
        camRight.Normalize();

        //-----移動入力をカメラ方向に合わせて変換-----
        Vector3 moveDir = camFowerd * inputMove.y + camRight * inputMove.x;

        ShowCamera1();

        //-----移動-----
        Vector3 move = moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
    void Jump()
    {
        //-----ジャンプ(地面の当たり判定)-----
        Vector3 checkPosition = transform.position + groundCheckOffset;
        isGround1 = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);

        isHighjump = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, player2Layer);
    }
    public void ShowCamera1()
    {
        if (cameraTrans != null)
        {
            //-----キャラの回転：常にカメラの正面に合わせる-----
            Vector3 lookDirection = -cameraTrans.forward;
            lookDirection.y = 0;
            lookDirection.Normalize();


            if (lookDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);

                if (modelTransform != null)
                {
                    Quaternion modelRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
                    modelRotation *= Quaternion.Euler(0, modelFacingOffsetY, 0); // ←補正ここ！
                    modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, modelRotation, 0.1f);
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGround1 ? Color.green : Color.red;
        Vector3 checkPosition = transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, Vector3.one * groundCheckRadius * 2f);
    }
}
