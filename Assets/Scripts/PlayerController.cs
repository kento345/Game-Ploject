using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [SerializeField,Header("�ړ����x")]
    private float speed_ = 5f; 
    private Vector2 inputMove;                  //���͂��ꂽ�ړ����� 
    //-----------�W�����v------------
    [SerializeField,Header("�W�����v��")] 
    private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGround = true;
    [SerializeField] 
    private LayerMask groundLayer;

    public void OnMove(InputAction.CallbackContext context)
    {
        //���͒l��ǂݎ��
        inputMove = context.ReadValue<Vector2>();  
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        //Vector2����Vector3�ɕύX
        Vector3 move = new Vector3 (inputMove.x, 0f ,inputMove.y) * speed_ * Time.deltaTime;
        //���݈ʒu�ɉ��Z���Ĉړ�
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
