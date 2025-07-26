using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;
    private bool isMove1 = false;

    [Header("�W�����v�ݒ�")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float highjumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask player2Layer;
    public bool isGround1 = false;
    public bool isHighjump = false;

    [Header("�����蔻��")]
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius;
    //-----�J����-----
    private Transform cameraTrans;

    //-----���̑�-----
    private Rigidbody rb;

    private void Start()
    {
        //-----������-----
        isGround1 = false;
        isHighjump = false;
        cameraTrans = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }
    //-----�ړ�����-----
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    //-----�W�����v����-----
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround1 && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (context.performed && isHighjump && rb != null)
        {
            rb.AddForce(Vector3.up * highjumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        
        if (rb != null)
        {
            Move();
            Jump();
        }
    }

    void Move()
    {
        //-----�J����-----
        Vector3 camFowerd = cameraTrans.forward;
        Vector3 camRight = cameraTrans.right;

        camFowerd.y = 0;
        camRight.y = 0;
        camFowerd.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camFowerd * inputMove.y + camRight * inputMove.x;

        isMove1 = moveDir.sqrMagnitude > 0.001f;

        if (isMove1) // �ړ����Ă���Ƃ�������]
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }

        //-----�ړ�-----
        Vector3 move = moveDir * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
    void Jump()
    {
        //-----�W�����v(�n�ʂ̓����蔻��)-----
        Vector3 checkPosition = transform.position + groundCheckOffset;
        isGround1 = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);

        isHighjump = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, player2Layer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGround1 ? Color.green : Color.red;
        Vector3 checkPosition = transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(checkPosition, Vector3.one * groundCheckRadius * 2f);
    }
}
