using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public int healthPoint = 3;
    public int coins = 3;
    public GameObject coinPrefab;

    private void Start()
    {
        PlayerManager.Instance.enemies.Add(gameObject);
    }

    void Update()
    {
        if(healthPoint == 0) 
        {
            for (int i = 0; i < coins; i++) 
            {
                var inst = Instantiate(coinPrefab);
                inst.transform.position = gameObject.transform.position;
            }
            PlayerManager.Instance.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet") 
        {
            healthPoint--;
        }
    }
}