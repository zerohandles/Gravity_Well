using UnityEngine;

public class FuelUpgrade : PowerUp
{
    [SerializeField] float _fuelAmount;
    [SerializeField] ParticleSystem _pickUpEffect;

    public override void UsePowerUp(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<PlayerMovement>(out var player))
        {
            player.AddFuel(_fuelAmount);
            Instantiate(_pickUpEffect, player.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
