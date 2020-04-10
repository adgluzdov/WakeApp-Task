using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public string collectorTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == collectorTag)
        {
            CoinsManager.Instance.Coins++;
            Destroy(gameObject);
        }
    }
}
