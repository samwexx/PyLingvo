using UnityEngine;
using UnityEngine.UI;
using TMPro;
    public class TerminalController : MonoBehaviour
{
  

    public GameObject terminalPanel;
    public TMP_InputField codeInput;
    public TMP_Text outputText;


    public static TerminalController instance;

    private System.Action<string> onCodeSubmit;




    public void TestClick()
    {
        Debug.Log("CLICK");
    }




    private void Start()
    {
        terminalPanel.SetActive(false);
    }



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Open(System.Action<string> onSubmit, string message = "")
    {
        onCodeSubmit = onSubmit;
        terminalPanel.SetActive(true);
        outputText.text = message;
        codeInput.text = "";
        codeInput.ActivateInputField();

        codeInput.Select();
        codeInput.ActivateInputField();
    }

    public void Close()
    {
        terminalPanel.SetActive(false);


    }

    public void RunCode()
    {
        string code = codeInput.text.Trim();

        if (onCodeSubmit != null)
        {
            onCodeSubmit.Invoke(code);
        }
    }

}

