using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CamInteract : MonoBehaviour, IInteractable
{
    [SerializeField] Text interactionText;

    [SerializeField] float interactDistance = 5f;
    [SerializeField] bool canInteract = true;


    [SerializeField] 
    void Start()
    {
        
    }

    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (canInteract == true)
        {
            Ray ray1 = new Ray(transform.position, transform.forward);
            RaycastHit hit1;

            if (Physics.Raycast(ray1, out hit1, interactDistance))
            {
                if (hit1.collider.CompareTag("Person"))
                {
                    interactionText.text = "Talk to him";

                    if (Input.GetMouseButton(0))
                    {
                        canInteract = false;
                        StartCoroutine(TalkToPersonCO());
                    }
                }
                else
                {
                    interactionText.text = "";
                }
            }
            else
            {
                interactionText.text = "";
            }
        }
    }

    IEnumerator TalkToPersonCO()
    {
        yield return new WaitForSeconds(2f);
    }
    
}
