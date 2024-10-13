using UnityEngine;

public class RepairUpgrade : PowerUp
{
    [SerializeField] float _repairVaue;
    [SerializeField] ParticleSystem _pickUpEffect;

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
