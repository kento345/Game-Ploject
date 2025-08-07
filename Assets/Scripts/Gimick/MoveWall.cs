using UnityEngine;

public class MoveWall : MonoBehaviour
{
    //-----ˆÚ“®-----
    private Vector3 moveOffset = new Vector3(20f,0f,0f);
    private float speed_ = 10f;

    private Vector3 startPos;
    private Vector3 targetPos;
    public bool shouldMove = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed_ * Time.deltaTime);
        }
        else if(shouldMove == false) 
        {
            transform.position = Vector3.MoveTowards(transform.position,startPos, speed_ * Time.deltaTime);
        }
    }
}
