using System.Text;

namespace TurnBasedUnits.Helpers
{
    public static class StringConverter
    {
        public static string GetUiPerkName(string perkName)
        {
            StringBuilder _stringBuilder = new StringBuilder();
            _stringBuilder.Append(perkName[0]);

            for (int i = 1; i < perkName.Length; i++)
            {
                if (char.IsUpper(perkName[i]))
                    _stringBuilder.Append(' ');

                _stringBuilder.Append(perkName[i]);
            }

            return _stringBuilder.ToString();
        }
    }
}
