using UnityEngine;

public class FlipByReferenceSprite : MonoBehaviour
{
    private SpriteRenderer parentSpriteRenderer;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = parentSpriteRenderer.flipX;
    }
}
