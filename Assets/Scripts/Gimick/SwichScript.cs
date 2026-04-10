using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

/*[System.Serializable]
public class ButtonPressedEvent : UnityEvent { }
*/
public class SwichScript : MonoBehaviour
{
    //[SerializeField] private ButtonPressedEvent OnButtonPressed;

    private float bottomY = -0.19f;
    private float speed = 0.5f;
    private float startY;
    private bool isPressed = false;
    private GameObject currentObject;

    private int targetLayer;
    public MoveWall wall_;

    private void Start()
    {
        startY = transform.position.y;
        targetLayer = LayerMask.NameToLayer("Player2");
    }

    private void Update()
    {
        // 乗っていたオブジェクトが消えた or 非アクティブになった
        if (currentObject != null && !currentObject.activeInHierarchy)
        {
            currentObject = null;
            isPressed = false;
        }
        // 押された → 徐々に下がる
        if (isPressed && transform.position.y > bottomY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            if (transform.position.y <= bottomY)
            {
                wall_.shouldMove = true;
            }
        }
        // 押されてない → 徐々に上がる
        else if (!isPressed && transform.localPosition.y < startY)
        {
            transform.localPosition += Vector3.up * speed * Time.deltaTime;
            if (transform.localPosition.y > startY)
            {
               wall_.shouldMove = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.gameObject.layer == targetLayer)
        {
            currentObject = other.gameObject;          
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == targetLayer)
        {
            isPressed = false;
        }
    }
}

