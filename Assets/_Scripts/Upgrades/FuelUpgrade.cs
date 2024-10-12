using UnityEngine;

public class FuelUpgrade : PowerUp
{
    [SerializeField] float _fuelAmount;

    public override void UsePowerUp(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<PlayerMovement>(out var player))
        {
            player.AddFuel(_fuelAmount);
            Destroy(this.gameObject);
        }
    }
}
