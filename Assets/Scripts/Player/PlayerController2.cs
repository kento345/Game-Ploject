using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController2 : MonoBehaviour
{

    [Header("�ړ��ݒ�")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;
    private bool isMove = false;

    [Header("�W�����v�ݒ�")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    public bool isGround2 = false;

    [Header("�����蔻��")]
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private float groundCheckRadius;

    //-----�J����-----
    private Transform cameraTrans;

    //-----���̑�-----
    private Rigidbody rb;

    private void Start()
    {
       
        isGround2 = false;
        rb= GetComponent<Rigidbody>();
        cameraTrans = Camera.main.transform;
    }
    //-----�ړ�����-----
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    //-----�W�����v����-----
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

            //-----�W�����v(�n�ʂ̓����蔻��)-----
            Vector3 checkPosition = transform.position + groundCheckOffset;
            isGround2 = Physics.CheckBox(checkPosition, Vector3.one * groundCheckRadius, Quaternion.identity, groundLayer);
        }
    }
    void Move()
    {
        //-----�J������]-----
        Vector3 camFowerd = cameraTrans.forward;
        Vector3 camRight = cameraTrans.right;

        camFowerd.y = 0;
        camRight.y = 0;
        camFowerd.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camFowerd * inputMove.y + camRight * inputMove.x;

        isMove = moveDir.sqrMagnitude > 0.001f;

        if (isMove) // �ړ����Ă���Ƃ�������]
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }

        //-----�ړ�-----
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
