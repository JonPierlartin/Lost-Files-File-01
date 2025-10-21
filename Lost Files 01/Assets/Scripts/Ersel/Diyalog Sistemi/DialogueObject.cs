// DialogueObject.cs
using UnityEngine;

// Bu sihirli satır, Unity'nin Assets > Create menüsüne yeni bir seçenek ekler.
// Artık Proje klasöründe sağ tıklayıp "Dialogue/New Dialogue" diyebileceğiz.
[CreateAssetMenu(fileName = "Yeni Diyalog", menuName = "Dialogue/New Dialogue")]
public class DialogueObject : ScriptableObject // MonoBehaviour değil, ScriptableObject!
{
    // Az önce yaptığımız "DialogueLine" sınıfından bir DİZİ (Array).
    // Yani bu konuşma, içinde birden fazla diyalog satırı tutacak.
    public DialogueLine[] lines;

    // Buraya gelecekte başka şeyler de ekleyebiliriz.
    // Mesela bu konuşma bitince bir sonraki konuşma ne olacak?
    // public DialogueObject nextDialogue;
    // Veya konuşma bitince bir event mi tetiklenecek?
    // public UnityEvent onDialogueEnd;
    // Şimdilik sadece 'lines' yeterli.
}