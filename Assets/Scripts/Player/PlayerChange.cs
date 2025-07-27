using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChange : MonoBehaviour
{
    [Header("プレイヤーオブジェクト")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public PlayerController1 PS1;
    public PlayerController2 PS2;

    [Header("カメラ切り替え")]
    private GameObject camera_;



    private void Awake()
    {
        PS1 = player1.GetComponent<PlayerController1>();
        PS2 = player2.GetComponent<PlayerController2>();
        // 両方のPlayerControllerを一度無効にする
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
    //-----Player2切り替え入力-----
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
