using UnityEngine;

// Infinitely loop the main menu background on the horizontal axis
public class InfiniteScrolling : MonoBehaviour
{
    [SerializeField] float _scrollSpeed;
    float _offset;
    Material _material;

    void Start() => _material = GetComponent<Renderer>().material;

    void Update()
    {
        _offset += (Time.deltaTime * _scrollSpeed) / 10;
        _material.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
    }
}
