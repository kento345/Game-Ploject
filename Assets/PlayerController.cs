using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    [SerializeField]private float speed_ = 5f;  //�ړ����x
    private Vector2 inputMove;                  //���͂��ꂽ�ړ����� 
    public void OnMove(InputAction.CallbackContext context)
    {
        //���͒l��ǂݎ��
        inputMove = context.ReadValue<Vector2>();  
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
