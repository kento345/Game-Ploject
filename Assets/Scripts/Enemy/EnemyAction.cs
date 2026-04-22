using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    [SerializeField] private Vector3 firpos;
    [SerializeField] private GameObject box;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ID1Action()
    {
        GameObject bullet = Instantiate(box, firpos, Quaternion.identity);

    }
}
