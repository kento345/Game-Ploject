using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController2 : MonoBehaviour
{

    [Header("移動設定")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;
    private bool isMove = false;

    [Header("ジャンプ設定")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    public bool isGround2 = false;

    [Header("当たり判定")]
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius;

    //-----カメラ-----
    private Transform cameraTrans;

    [Header("キャラクター補正")]
    [SerializeField] private Transform modelTransform;
    private float modelFacingOffsetY = 170f;

    //-----その他-----
    private Rigidbody rb;
   

    private void Start()
    {
       
        isGround2 = false;
        rb= GetComponent<Rigidbody>();
        cameraTrans = Camera.main.transform;
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
            Move();

            //-----ジャンプ(地面の当たり判定)-----
            Vector3 checkPosition = transform.position + groundCheckOffset;
            isGround2 = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);
        }
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

        //isMove = moveDir.sqrMagnitude > 0.001f;

        //-----キャラの回転：常にカメラの正面に合わせる-----
        Vector3 lookDirection = cameraTrans.forward;
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

        //-----移動-----
        Vector3 move = moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGround2 ? Color.green : Color.red;
        Vector3 checkPosition = transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, Vector3.one * groundCheckRadius * 2f);
    }
}
