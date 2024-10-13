using UnityEngine;

public class EngineUpgrade : PowerUp
{
    [SerializeField] ParticleSystem _pickUpEffect;

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
