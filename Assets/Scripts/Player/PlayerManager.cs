using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonOfScene<PlayerManager>
{
    public GameObject player;
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject targetEnemy;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = Mathf.Infinity;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(player.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetEnemy = enemy;
            }
            else 
            {
                if (enemy == targetEnemy) {
                    targetEnemy = null;
                }
            }
        }
    }

}
