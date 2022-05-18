using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace wpfEFac.Helpers
{
    public class Validador
    {
        public static void TextBoxNumeroEntero(TextBox txtBox, String errorMessage)
        {
            try
            {
                ValidarExprecion(int.Parse(txtBox.Text) >= int.MinValue, errorMessage);
            }
            catch (Exception)
            {
                throw new Exception(errorMessage);
            }

        }

        public static bool ValidarCorreoElectronico(string email) 
        {
            string mailpattern = @"^(([^<>()[\]\\.,;:\s@\""]+" +
            @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" +
            @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" +
            @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" +
            @"[a-zA-Z]{2,}))$";

            Regex mailRegex = new Regex(mailpattern);

            try
            {
                ValidarExprecion(mailRegex.IsMatch(email), "Invalid Email");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void ValidarExprecion(bool expresion, string errorMessage)
        {
            if (!expresion)
            {
                throw new Exception(errorMessage);
            }
        }

        public static void ShowError(Control ctrlError, string message)
        {
            ctrlError.BorderBrush = new SolidColorBrush(Colors.Red);
            var tooltip = new ToolTip() { 
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Colors.Red),
                Content = message
            };

            ctrlError.ToolTip = tooltip;
        }

        public static void ClearError(Control ctrl) 
        {
            LinearGradientBrush borde = new LinearGradientBrush();
            borde.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xFF, 0xA3, 0xAE, 0xB9), Offset = 0 });
            borde.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xFF, 0x83, 0x99, 0xA9), Offset = 0.375 });
            borde.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xFF, 0x71, 0x85, 0x97), Offset = 0.375 });
            borde.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xFF, 0x61, 0x75, 0x84), Offset = 1 });

            ctrl.BorderBrush = borde;
            ctrl.ToolTip = null;
        }


        public static void TextBoxNumeroDecimal(TextBox txtBox, String errorMessage)
        {
            try
            {
                ValidarExprecion(decimal.Parse(txtBox.Text) != decimal.Parse("0.000000001".ToString()), errorMessage);
            }
            catch (Exception)
            {
                throw new Exception(errorMessage);
            }
        }

        public static bool TryParseTextBoxToDecimal(string value)
        {
            try
            {
                decimal.Parse(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidarRFC(string valor) 
        {
            return (string.IsNullOrWhiteSpace(valor)) && (valor.Length >= 12 && valor.Length >= 13);
        }
    }
}
