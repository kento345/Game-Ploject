using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChange : MonoBehaviour
{
    [Header("�v���C���[�I�u�W�F�N�g")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public PlayerController1 PS1;
    public PlayerController2 PS2;

    [Header("�J�����؂�ւ�")]
    private GameObject camera_;



    private void Awake()
    {
        PS1 = player1.GetComponent<PlayerController1>();
        PS2 = player2.GetComponent<PlayerController2>();
        // ������PlayerController����x�����ɂ���
        PS1.enabled = false;
        PS2.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         PS1.enabled = true;
    }

    public void OnChar1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PS1.enabled = true ;
            PS2.enabled = false;
            if (! PS2.enabled)
            {
                PS2.isGround2 = false ;
            }
        }
    }
    //-----Player2�؂�ւ�����-----
    public void OnChar2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PS1.enabled= false;
            PS2.enabled= true ;
            if(! PS1.enabled)
            {
                PS1.isGround1 = false ;
                PS1.isHighjump = false ;
            }
        }
    }

}
