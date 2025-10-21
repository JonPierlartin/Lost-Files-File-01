// DialogueTrigger.cs
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Hangi "DVD'yi" (DialogueObject) oynatacağız?
    // Buraya Inspector'dan o .asset dosyasını sürükleyeceğiz.
    public DialogueObject dialogueToPlay;

    // Diyaloğu başlatmak için basit bir yol:
    // Bu objeye tıklandığında...
    private void OnMouseDown()
    {
        // DialogueManager'ımıza (Singleton) ulaşıp ona "Bu diyaloğu başlat" diyoruz.
        DialogueManager.instance.StartDialogue(dialogueToPlay);
    }

    // Not: OnMouseDown() sadece üzerinde Collider (örn: BoxCollider)
    // olan nesnelerde çalışır.
}