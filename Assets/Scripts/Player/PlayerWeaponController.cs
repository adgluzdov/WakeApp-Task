using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public float force = 10f;
    public float reloadTime = 1f;
    public float maxDistance = 5f;
    public LayerMask layerMask;
    public GameObject bulletPrefab;
    private float reloadTimeSpent;
    private GameObject bulletInstance;
    
    void Start()
    {
        bulletInstance = Instantiate(bulletPrefab, transform);
    }

    void Update()
    {
        if (Joystick.Instance.Vertical != 0 || Joystick.Instance.Horizontal != 0) {
            reloadTimeSpent = reloadTime;
            return;
        }

        if (reloadTimeSpent > 0) 
        {
            reloadTimeSpent -= Time.deltaTime;
        }
        else 
        {
            if (bulletInstance == null)
            { 
                bulletInstance = Instantiate(bulletPrefab, transform);
            }
            if (PlayerManager.Instance.targetEnemy != null) 
            {
                var direction = PlayerManager.Instance.player.transform.forward;
                bulletInstance.transform.parent = null;
                bulletInstance.transform.rotation = Quaternion.LookRotation(direction);
                bulletInstance.GetComponent<Rigidbody>().isKinematic = false;
                bulletInstance.GetComponent<Rigidbody>().AddForce(direction.normalized * force, ForceMode.Impulse);

                reloadTimeSpent = reloadTime;
                bulletInstance = null;
            }
        }
    }
}
