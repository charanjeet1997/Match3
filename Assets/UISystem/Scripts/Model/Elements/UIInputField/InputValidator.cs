using System.Text.RegularExpressions;
using UnityEngine;

namespace ModulerUISystem
{
    [CreateAssetMenu(menuName = "Validator/InputValidator", fileName = "InputValidator")]
    public class InputValidator : ScriptableObject
    {
       

        public string ErrorDescription => errorDescription;

        [SerializeField] private string errorDescription = "Please enter valid email address.";
        [SerializeField] private string regexPattern =
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        [SerializeField] private bool isDebugging;
        [SerializeField] private bool defaultReturnValue;

        public virtual bool Validate(string textToBeValidated)
        {
            Debug.Log(textToBeValidated);
            if (isDebugging)
            {
                return defaultReturnValue;
            }
            else
            {
                return Regex.IsMatch(textToBeValidated,
                    regexPattern,
                    RegexOptions.IgnoreCase);
            }
        }
    }
}