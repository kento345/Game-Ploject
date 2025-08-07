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
        // ���������XZ���ʂɌ���i�㉺�𖳎��j
        Vector3 backward = target.transform.right;
        backward.y = 0;
        backward.Normalize();

        // �Ǐ]�ڕW�F�^�[�Q�b�g�̌��5m�iXZ���ʏ�j
        Vector3 targetPos = target.transform.position + backward * 5.0f;

        targetPos.y = 1.0f; 

        // �t�H�����[��Y���W���ێ��i�����ς��Ȃ��j
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
