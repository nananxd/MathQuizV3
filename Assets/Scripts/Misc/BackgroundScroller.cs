using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    private RawImage bgImage;
    private Renderer renderer;
    [SerializeField] private Vector2 savedOffset;
    // Start is called before the first frame update
    void Start()
    {
        bgImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        bgImage.uvRect = new Rect(bgImage.uvRect.position + new Vector2(savedOffset.x, savedOffset.y) * Time.deltaTime, bgImage.uvRect.size);
    }
}
