using UnityEngine;

public class RepairUpgrade : PowerUp
{
    [SerializeField] float _repairVaue;

    public override void UsePowerUp(GameObject gameObject)
    {
        var player = gameObject.transform.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.GainHealth(_repairVaue);
            // play particle effect
            Destroy(this.gameObject);
        }
    }
}
