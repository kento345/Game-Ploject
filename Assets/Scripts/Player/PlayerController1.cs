using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [SerializeField] private float speed = 5f;
    private Vector2 inputMove;

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

    [Header("�L�����N�^�[�␳")]
    [SerializeField] private Transform modelTransform;
    private float modelFacingOffsetY = 170f;

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
        //-----�J������]-----
        Vector3 camFowerd = cameraTrans.forward;
        Vector3 camRight = cameraTrans.right;

        camFowerd.y = 0;
        camRight.y = 0;
        camFowerd.Normalize();
        camRight.Normalize();

        //-----�ړ����͂��J���������ɍ��킹�ĕϊ�-----
        Vector3 moveDir = camFowerd * inputMove.y + camRight * inputMove.x;

        ShowCamera1();

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
    public void ShowCamera1()
    {
        if (cameraTrans != null)
        {
            //-----�L�����̉�]�F��ɃJ�����̐��ʂɍ��킹��-----
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
                    modelRotation *= Quaternion.Euler(0, modelFacingOffsetY, 0); // ���␳�����I
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
