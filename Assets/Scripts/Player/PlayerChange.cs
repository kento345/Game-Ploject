using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerChange : MonoBehaviour
{
    [Header("プレイヤーオブジェクト")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    private  PlayerController1 PS1;
    private PlayerController2 PS2;

    public bool isPlayer1;
    public bool isPlayer2;

    [Header("カメラ切り替え")]
    private GameObject camera_;

    [SerializeField] private Image crossher;

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
        isPlayer1 = true;
        PS1.enabled = true;
    }
    //-----Player1切り替え入力-----
    public void OnChar1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PS1.enabled = true ;
            PS2.enabled = false;
            crossher.enabled = true;
            isPlayer1 = true;
            isPlayer2 = false;
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
            PS1.enabled = false;
            PS2.enabled = true ;
            crossher.enabled = false;
            isPlayer2 = true;
            isPlayer1 = false;
            if (! PS1.enabled)
            {
                PS1.isGround1 = false ;
                PS1.isHighjump = false ;
            }
        }
    }

}
