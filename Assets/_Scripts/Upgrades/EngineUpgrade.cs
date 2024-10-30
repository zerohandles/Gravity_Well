using UnityEngine;

public class EngineUpgrade : PowerUp
{
    [SerializeField] ParticleSystem _pickUpEffect;

    // Display particle effect on pickup and upgrade the player's engine
    public override void UsePowerUp(GameObject gameObject)
    {
        var player = gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.UpgradeEngine();
            Instantiate(_pickUpEffect, player.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
