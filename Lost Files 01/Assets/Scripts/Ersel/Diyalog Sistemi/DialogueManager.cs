// DialogueManager.cs
using System.Collections.Generic; // Queue (kuyruk) için bu gerekli
using UnityEngine;
using TMPro; // TextMeshPro kullanıyorsak bu gerekli
using UnityEngine.UI; // Image ve Button için bu gerekli

public class DialogueManager : MonoBehaviour
{
    // --- UI Referansları ---
    // Inspector'dan sürükleyip bırakacağız.
    [Header("UI Elementleri")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lineText;
    public Button nextButton; // "İleri" butonu

    // --- Diyalog Takibi ---
    // Gösterilecek satırları bir "kuyruğa" alacağız.
    // Kuyruk (Queue) şu işe yarar: İlk giren ilk çıkar (FIFO).
    // Bize tam olarak bu lazım.
    private Queue<DialogueLine> lineQueue;

    // Singleton (Oynatıcıya her yerden kolay erişim için basit bir yol)
    // Başka script'ler `DialogueManager.instance.StartDialogue()` diyebilsin diye.
    public static DialogueManager instance;

    void Awake()
    {
        // Singleton ayarı
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Kuyruğu başlatalım
        lineQueue = new Queue<DialogueLine>();
    }

    void Start()
    {
        // Butona tıklandığında ne olacağını ayarlayalım.
        // "İleri" butonuna her basıldığında "DisplayNextLine" fonksiyonunu çalıştır.
        nextButton.onClick.AddListener(DisplayNextLine);
    }


    // === BU EN ÖNEMLİ FONKSİYON ===
    // Dışarıdan birisi (mesela bir NPC) bu fonksiyonu çağıracak
    // ve hangi "DVD'yi" (DialogueObject) oynatacağını söyleyecek.
    public void StartDialogue(DialogueObject dialogue)
    {
        // 1. Paneli görünür yap
        dialoguePanel.SetActive(true);

        // 2. Eski konuşmadan kalanları temizle
        lineQueue.Clear();

        // 3. Oynatacağımız DVD'nin (DialogueObject) içindeki
        //    tüm "lines" (satırları) tek tek kuyruğa ekle (Enqueue).
        foreach (DialogueLine line in dialogue.lines)
        {
            lineQueue.Enqueue(line);
        }

        // 4. İlk satırı göster
        DisplayNextLine();
    }

    // "İleri" butonuna basıldığında çalışan fonksiyon
    public void DisplayNextLine()
    {
        // 1. Kuyrukta hiç satır kalmadıysa, konuşmayı bitir.
        if (lineQueue.Count == 0)
        {
            EndDialogue();
            return; // Fonksiyondan çık
        }

        // 2. Kuyrukta satır varsa:
        //    Kuyruktan bir satır çek (Dequeue). Bu, o satırı kuyruktan siler.
        DialogueLine currentLine = lineQueue.Dequeue();

        // 3. UI elementlerini bu satırdaki bilgilerle doldur.
        nameText.text = currentLine.speakerName;
        lineText.text = currentLine.lineText;
    }

    // Konuşma bittiğinde
    public void EndDialogue()
    {
        // 1. Paneli gizle
        dialoguePanel.SetActive(false);
        Debug.Log("Diyalog bitti.");
    }
}