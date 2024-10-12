using UnityEngine;

public class EngineUpgrade : PowerUp
{
    public override void UsePowerUp(GameObject gameObject)
    {
        var player = gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.UpgradeEngine();
            Destroy(this.gameObject);
        }
    }
}
