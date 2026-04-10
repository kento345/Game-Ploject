using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData")]
public class Data:ScriptableObject
{
    [SerializeField] private int ID;

    public int GetID() => ID;
}
