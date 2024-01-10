namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal static class DataValidator
    {
        public static class PilotName
        {
            private const string PILOTNAME_MUST_BE_SET = "Pilot name must be set.";

            public static string IsValid(string value)
            {
                if (string.IsNullOrEmpty(value) ||
                               string.IsNullOrWhiteSpace(value))
                {
                    return PILOTNAME_MUST_BE_SET;
                }

                return null;
            }
        }

        public static class ApiAddress
        {
            private const string VALIDATION_APIADDRESS_MUST_BE_SET = "API address must be set.";
            private const string VALIDATION_APIADDRESS_URI_FORMAT = "API URI format is invalid. Should look like http://domain.ext or http://domain.ext";

            public static string IsValid(string value)
            {
                if (string.IsNullOrEmpty(value) ||
                         string.IsNullOrWhiteSpace(value))
                {
                    return VALIDATION_APIADDRESS_MUST_BE_SET;
                }
                bool isFormatValid = SettingsValidators.IsUriValid(value);

                if (!isFormatValid)
                {
                    return VALIDATION_APIADDRESS_URI_FORMAT;
                }

                return null;
            }
        }

        public static class PersonalKey
        {
            private const string VALIDATION_PERSONALKEY_LENGTH_INVALID = "Personal key length should be at least 10 character long.";
            private const string VALIDATION_PERSONALKEY_FORMAT_INVALID = "Personal should be made of alphanumerical character and \"-\", \"_\", \"@\".";

            public static string IsValid(string value)
            {
                if (string.IsNullOrEmpty(value) ||
                         string.IsNullOrWhiteSpace(value))
                {
                    return VALIDATION_PERSONALKEY_FORMAT_INVALID;
                }

                if (value.Length < 10)
                {
                    return VALIDATION_PERSONALKEY_LENGTH_INVALID;
                }

                return null;
            }
        }

        public static class CarName
        {
            private const string CARNAME_MUST_BE_SET = "Car name must be set.";

            public static string IsValid(string value)
            {
                if (string.IsNullOrEmpty(value) ||
                           string.IsNullOrWhiteSpace(value))
                {
                    return CARNAME_MUST_BE_SET;
                }
                return null;
            }
        }
    }
}
