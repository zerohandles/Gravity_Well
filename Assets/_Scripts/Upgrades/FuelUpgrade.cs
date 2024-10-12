using UnityEngine;

public class FuelUpgrade : PowerUp
{
    [SerializeField] float _fuelAmount;

    public override void UsePowerUp(GameObject gameObject)
    {
        var player = gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.AddFuel(_fuelAmount);
            Destroy(this.gameObject);
        }
    }
}
