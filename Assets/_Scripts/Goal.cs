using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event Action TriggerVictory;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TriggerVictory?.Invoke();
    }
}
