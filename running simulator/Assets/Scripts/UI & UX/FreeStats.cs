using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeStats : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Player.Instance.AddCoins(5000000);
        Player.Instance.SetSpeed(10);

        gameObject.SetActive(false);
    }
}
