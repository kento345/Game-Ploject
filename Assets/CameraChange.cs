using System.Globalization;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraChange : MonoBehaviour
{
    [Header("í«è]ëŒè€")]
    [SerializeField] private Transform p1; //Player1
    [SerializeField] private Transform p2; //Player2

    
   //CinemachineCamera
    private CinemachineCamera cinemaCamera;

   
    public void OnCamera1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            cinemaCamera.Follow = p1;
            cinemaCamera.LookAt = p1;
        }
    }
    public void OnCamera2(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            cinemaCamera.Follow = p2;
            cinemaCamera.LookAt = p2;
        }
    }
    private void Start()
    {
        cinemaCamera = GetComponent<CinemachineCamera>();
    }
    
    private void Update()
    { 
       
    }
}
