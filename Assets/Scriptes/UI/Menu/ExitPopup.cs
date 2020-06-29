using Subsystem.Question;
using UnityEngine;
using UnityEngine.UI;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private InputField _passwordInputField;
    [SerializeField] private string _password = "123";

    private void Awake()
    {
        _okButton.onClick.AddListener(OnOkButtonClicked);
        _cancelButton.onClick.AddListener(OnCancelButtonClicked);
    }

    private void OnOkButtonClicked()
    {
        if (_passwordInputField.text == _password)
        {
            Debug.Log("Goodbye");
            //Application.Quit();
        }
        _passwordInputField.text = "";
    }

    private void OnCancelButtonClicked()
    {
        MenuManager.ExitPopup.SetActive(false);
    }
}