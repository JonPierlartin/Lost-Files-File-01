using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Image cursorImage;      // Canvas altındaki Image
    public Sprite emptySprite;     // Normal cursor
    public Sprite fullSprite;      // Interactable objelere bakınca

    public float rayDistance = 3f; // Etkileşim mesafesi
    public LayerMask interactableLayer; // Interactable objelerin layer'ı

    void Start()
    {
        // Başlangıçta empty sprite
        if(cursorImage != null && emptySprite != null)
            cursorImage.sprite = emptySprite;

        // Sistem cursorunu gizle
        Cursor.visible = false;
    }

    void Update()
    {
        if (cursorImage == null) return; // Hata önleme

        // Kameradan ileriye doğru raycast
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            if(fullSprite != null)
                cursorImage.sprite = fullSprite; // Interactable objeye bakınca full
        }
        else
        {
            if(emptySprite != null)
                cursorImage.sprite = emptySprite; // Normalde empty
        }
    }
}
