using UnityEngine;
public class MapPartController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
}
