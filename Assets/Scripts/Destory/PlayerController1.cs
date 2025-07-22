using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [SerializeField, Header("�ړ����x")]
    private float speed_ = 5f;
    private Vector2 inputMove;                  //���͂��ꂽ�ړ����� 
    //-----------�W�����v------------
    [SerializeField, Header("�W�����v��")]
    private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGround = true;
    [SerializeField]
    private LayerMask groundLayer;
    //--------------Script�`�F���W--------------    
    private PlayerController1 myScript1;
    [SerializeField] private bool isJump1;
    private PlayerController2 myScript2;
    private bool isJump2;

    public void OnMove(InputAction.CallbackContext context)
    {
        //���͒l��ǂݎ��
        inputMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGround && isJump1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }
    public void OnChar1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            myScript1.enabled = true;
            myScript2.enabled = false;
            isJump1 = true;
            isJump2 = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myScript1 = GetComponent<PlayerController1>();
        myScript2 = GetComponent<PlayerController2>();
        isJump2   = GetComponent<PlayerController2>();
    }

    // Update is called once per frame
    void Update()
    {
        Move1();
    }
    void Move1()
    {
        //Vector2����Vector3�ɕύX
        Vector3 move = new Vector3(inputMove.x, 0f, inputMove.y) * speed_ * Time.deltaTime;
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
