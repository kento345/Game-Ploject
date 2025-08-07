using System.Transactions;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField] private GameObject player_1 = null;
    [SerializeField] private GameObject player_2 = null;

    private PlayerChange Changeplayer_;

    bool isTracking = false;

    private void Start()
    {
        Changeplayer_ = GetComponent<PlayerChange>();
    }

    public void OnPlayerTrack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isTracking = !isTracking;
        }
    }

    private void Update()
    {
        if (isTracking)
        {
            Tracking();
        }
    }

    void Tracking()
    {
        if(Changeplayer_.isPlayer1)
        {
            FollowerToTarget(player_2, player_1);
        }
        else if(Changeplayer_.isPlayer2)
        {
            FollowerToTarget(player_1, player_2);
        }
    }

    void FollowerToTarget(GameObject follower , GameObject target)
    {
        // 後方方向をXZ平面に限定（上下を無視）
        Vector3 backward = target.transform.right;
        backward.y = 0;
        backward.Normalize();

        // 追従目標：ターゲットの後方5m（XZ平面上）
        Vector3 targetPos = target.transform.position + backward * 5.0f;

        targetPos.y = 1.0f; 

        // フォロワーのY座標を維持（高さ変えない）
        targetPos.y = follower.transform.position.y;

        Vector3 dist = targetPos - follower.transform.position;
        dist.y = 0f;
        float length = dist.magnitude;

        if (length < 0.05f)
            return;

        Vector3 vector = dist.normalized * (length / 20.0f);
        follower.transform.position += vector;

        Vector3 pos = follower.transform.position;
        pos.y = 1.0f;
        follower.transform.position = pos;
    }
}
