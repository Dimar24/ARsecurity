using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ExitView : View
    {
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private InputField _passwordInputField;
        [SerializeField] private string _password = "123";

        protected override void OnCreate()
        {
            _okButton.onClick.AddListener(OnOkButtonClicked);
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
            base.OnCreate();
        }

        private void OnOkButtonClicked()
        {
            if (_passwordInputField.text == _password)
            {
                Application.Quit();
            }
            _passwordInputField.text = "";
        }

        private void OnCancelButtonClicked()
        {
            ViewManager.OpenMainView();
        }
    }
}