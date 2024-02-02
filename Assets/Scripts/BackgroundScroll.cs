using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    private RawImage bgImage;
    private Renderer renderer;
    [SerializeField] private Vector2 savedOffset;

    void Start()
    {
        // renderer = GetComponent<Renderer>();
        bgImage = GetComponent<RawImage>();
    }

    void Update()
    {
        // float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        // Vector2 offset = new Vector2(x, 0);
        // renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        bgImage.uvRect = new Rect(bgImage.uvRect.position + new Vector2(savedOffset.x, savedOffset.y) * Time.deltaTime, bgImage.uvRect.size);
    }
}
