using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    [TextArea] public string instruction;
    [TextArea] public string correctAnswer;
    public bool questCompleted = false;


    public SpriteRenderer stoneRenderer;
    public Color beforeColor = Color.white;
    public Color afterColor = Color.green;



    private void Start()
    {

        if (stoneRenderer != null)
            stoneRenderer.color = beforeColor;


    }



    public void Interact()
    {
        if (!questCompleted)
        {
            TerminalController.instance.Open(CheckCode, instruction);
        }
        else
        {
            TerminalController.instance.Open(null, "Tu jau izpildīji!");
        }
    }

    private void CheckCode(string code)
    {
        string clean = code.Replace(" ", "").Replace("\n", "");
        string cleanCorrect = correctAnswer.Replace(" ", "").Replace("\n", "");

        if (clean == cleanCorrect)
        {
            questCompleted = true;
            TerminalController.instance.outputText.text = "Pareizi!";
            ExpManager.instance.GainExperience(3);

            TerminalController.instance.Close();

            // maina krāsu
            if (stoneRenderer != null)
                stoneRenderer.color = afterColor;
        }
        else
        {
            TerminalController.instance.outputText.text = "Nepareizi! :(";
        }
    }
}
