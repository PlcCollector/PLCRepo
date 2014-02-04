using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public static class ValidateInput
    {
        #region checksConfigPLC

        public static bool  CheckIfNameInputIsValid(string plcName ,out string errorMessage) 
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(plcName) || !(Encoding.UTF8.GetByteCount(plcName).Equals(plcName.Length)))
            {
                errorMessage = "Der PLC Name ist Leer oder enthält Sonderzeichen";
                return false;
            }

            if (plcName.Length > 20)
            {
               errorMessage = "Der PLC Name ist zu lang";
                return false;
            }

            return true;
        }

        public static bool CheckIfValueIsIntegerMaxMin(string integerToConvert, out string errorMessage, int MaxValue, int MinValue)
        {
            int resultTryParse;
            errorMessage = string.Empty;
            bool isNumber = int.TryParse(integerToConvert, out resultTryParse);

            if (!isNumber)
            {
                errorMessage = "Die Eingabe muss aus einer ganzen Zahl bestehen";
                return false;
            }

            if (resultTryParse < MinValue || resultTryParse > MaxValue)   
            {
                errorMessage = "Die eingegebene Zahl befindet sich außerhalb des wählbaren Bereichs ";
                return false;
            }

            return true;

        }

        public static bool CheckIFStringIsValidIPAdress(string ipAdress, out string errorMessage)
        {
            
            bool isValid = false;
            int i, resultTryParse;
            string[] adressParts = ipAdress.Split('.');
            errorMessage = string.Empty;

            for (i = 0; i < adressParts.Length; i++)
            {
                if (adressParts.Length == 4 && int.TryParse(adressParts[i], out resultTryParse) && resultTryParse < 255 && resultTryParse >= 0)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    errorMessage = "IP-Adresse wurde falsch eingegeben!! ";
                    break;
                }
                
            }

            return isValid;
        }

        public static bool CheckIfIntervalInputIsValid(string intervall, out string errorMessage )
        {
            int resultTryParse;
            errorMessage = string.Empty;
            bool isNumber = int.TryParse(intervall, out resultTryParse);

            if (resultTryParse > 86400000 || intervall.Length >= 9)   //Limit Sample Intervall = 86400000 ms = 1Day
            {
                errorMessage = "Das Intervall ist zu groß. max. 1Tag";
            }

            if (!isNumber)
            {
                errorMessage = "Das Intervall ist kein ganzzahliger Wert ";
                return false;
            }

            return true;
        }

        public static bool CheckIfPortInputIsValid(string port, out string errorMessage)
        {
            int resultTryParse;
            errorMessage = string.Empty;
            bool isNumber = int.TryParse(port, out resultTryParse);

            if (!isNumber)
            {
                errorMessage = "Der Port muss aus einer ganzen Zahl bestehen";
                return false;
            }

            if (resultTryParse <= 0 || resultTryParse > 65535)   // available Ports 0 - 65535
            {
                errorMessage = "Dieser Port steht nicht zur Verfügung. Es stehen nur Ports von 0 - 65535 zur Verfügung";
                return false;
            }
            return true;
        }

        public static bool CheckIfPLCTypeInputIsValid(string plcInputType, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(plcInputType))
            {
                errorMessage = "Es wurde kein PLC-Typ ausgewählt";
                return false;
            }
            return true;
        }

        public static bool CheckIfVariableTypeInputIsValid(string plcVariableType, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(plcVariableType))
            {
                errorMessage = "Es wurde kein Variablen-Typ ausgewählt";
                return false;
            }
            return true;
        }

        #endregion


    }
}
