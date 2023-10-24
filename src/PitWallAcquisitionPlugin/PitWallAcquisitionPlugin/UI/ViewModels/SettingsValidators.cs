using System;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public static class SettingsValidators
    {

        public static bool IsUriValid(string apiAddress)
        {
            Uri convetedUri;
            return CanCreateUri(apiAddress, out convetedUri)
                && convetedUri != null && (
                    convetedUri.Scheme == Uri.UriSchemeHttp
                    || convetedUri.Scheme == Uri.UriSchemeHttps);
        }

        private static bool CanCreateUri(
            string apiAddress, 
            out Uri convetedUri)
        {
            return Uri.TryCreate(
                            apiAddress,
                            UriKind.Absolute,
                            out convetedUri);
        }
    }
}