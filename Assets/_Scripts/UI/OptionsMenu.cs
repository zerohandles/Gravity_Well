using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioSource _playerAudio;
    [SerializeField] AudioSource _gameAudio;
    [SerializeField] Slider _masterVolume;
    [SerializeField] Slider _engineVolume;
    
    public void ChangeMasterVolume()
    {
        _gameAudio.volume = _masterVolume.value;
    }

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
}
