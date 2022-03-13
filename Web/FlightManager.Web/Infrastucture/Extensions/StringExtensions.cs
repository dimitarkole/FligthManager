namespace FlightManager.Web.Infrastucture.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class StringExtensions
    {
        public static string InsertSpaceBeforeUppercaseLetter(this string text)
        {
            string tempText = text;
            int insertedValues = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    tempText = tempText.Insert(i + insertedValues, " ");
                    insertedValues++;
                }
            }

            return tempText;
        }

        public static string ToControllerName(this string controller) => controller.Replace("Controller", "");
    }
}
