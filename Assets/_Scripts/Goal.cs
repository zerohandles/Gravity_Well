using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event Action TriggerVictory;

    // Trigger victory event when player reaches the goal zones.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            TriggerVictory?.Invoke();
    }
}
