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

    private int targetLayer;
    public MoveWall wall_;

    private void Start()
    {
        startY = transform.position.y;
        targetLayer = LayerMask.NameToLayer("Player2");
    }

    private void Update()
    {
        // ‰Ÿ‚³‚ê‚½ ¨ ™X‚É‰º‚ª‚é
        if (isPressed && transform.position.y > bottomY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            if (transform.position.y <= bottomY)
            {
                wall_.shouldMove = true;
            }
        }
        // ‰Ÿ‚³‚ê‚Ä‚È‚¢ ¨ ™X‚Éã‚ª‚é
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

