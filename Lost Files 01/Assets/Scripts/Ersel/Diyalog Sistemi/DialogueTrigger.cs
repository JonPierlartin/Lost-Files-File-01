using System.Collections;
using UnityEngine;
using TMPro;  // TextMeshPro kullanmak için

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 6)]
    public string[] dialogueLines;   // NPC'nin söyleyeceği replikler
    public GameObject dialogueUI;    // Diyalog UI paneli
    public TMP_Text dialogueText;    // TextMeshPro objesi
    public float textSpeed = 0.03f;  // Yazı efekti hızı

    private bool isDialogueActive = false;
    private PlayerMovement player; 
    [SerializeField] GameObject UIelements;  // Oyuncu hareket script'ine erişim

    void Start()
    {
        dialogueUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovement>();
            StartCoroutine(StartDialogue());
        }
    }

    IEnumerator StartDialogue()
    {
        if (isDialogueActive) yield break;
        isDialogueActive = true;
        dialogueUI.SetActive(true);

        // Oyuncunun hareketini durdur
        if (player != null) player.enabled = false;

        foreach (string line in dialogueLines)
        {
            dialogueText.text = "";
            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }

            // Bir sonraki cümleye geçmeden biraz bekle
            yield return new WaitForSeconds(2f);
        }

        // Diyalog bittiğinde
        dialogueUI.SetActive(false);
        if (player != null) player.enabled = true;
        isDialogueActive = false;
    }
}

