using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioSource _playerAudio;
    [SerializeField] AudioSource _gameAudio;
    [SerializeField] Slider _masterVolume;
    [SerializeField] Slider _engineVolume;

    // Change the background music and sound effects volume 
    public void ChangeMasterVolume() => _gameAudio.volume = _masterVolume.value;

    // Change only the player's engine noise volume
    public void ChangeEngineVolume()
    {
        _playerAudio.volume = _engineVolume.value;
        _playerAudio.volume = Mathf.Clamp(_playerAudio.volume, 0f, .75f);
    }

    public void Mute(bool isMuted)
    {
        _playerAudio.mute = isMuted;
        _gameAudio.mute = isMuted;
    }

    // Toggle between the player automatically moving forward or manual movement
    public void ToggleAutoMovement(bool enabled)
    {
        var player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (player != null )
            player.ToggleAutoMove(enabled);
    }
}
