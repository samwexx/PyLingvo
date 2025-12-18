using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;

    public bool isDialogueActive;

    private DialogueSO currentDialogue;
    private int dialogueIndex;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
            button.gameObject.SetActive(false);

        
    }

   public void StartDialogue(DialogueSO dialogueSO)
    {
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }


    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
            ShowDialogue();
        else
            ShowChoices();
    }




    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        portrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;
            
        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;
    }




    private void ShowChoices()
    {
        ClearChoices();

        // если есть варианты
        if (currentDialogue.options != null && currentDialogue.options.Length > 0)
        {
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];

                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].onClick.AddListener(() =>
                {
                    if (option.nextDialogue != null)
                        ChooseOption(option.nextDialogue);
                    else
                        EndDialogue();
                });

                choiceButtons[i].gameObject.SetActive(true);
            }
        }
        else
        {
            // если вариантов нет — показываем END
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "Iziet";
            choiceButtons[0].onClick.AddListener(EndDialogue);
            choiceButtons[0].gameObject.SetActive(true);
        }
    }




    private void ChooseOption(DialogueSO dialogueSO)
    {
        if (dialogueSO == null)
            EndDialogue();
        else
        {
            ClearChoices();
            StartDialogue(dialogueSO);
        }
    }
  




    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void ClearChoices()
    {
        foreach (var btn in choiceButtons)
        {
            btn.onClick.RemoveAllListeners();
            btn.gameObject.SetActive(false);
        }
    }



    //private void ClearChoices()
    //{ foreach (var button in choiceButtons) {
            //button.gameObject.SetActive(false);
            //button.onClick.RemoveAllListeners(); } }
}
