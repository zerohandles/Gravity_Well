using UnityEngine;

public class RepairUpgrade : PowerUp
{
    [SerializeField] float _repairVaue;
    [SerializeField] ParticleSystem _pickUpEffect;

    // Display particle effect on pickup and restores player's health
    public override void UsePowerUp(GameObject gameObject)
    {
        var player = gameObject.transform.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.GainHealth(_repairVaue);
            Instantiate(_pickUpEffect, player.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
