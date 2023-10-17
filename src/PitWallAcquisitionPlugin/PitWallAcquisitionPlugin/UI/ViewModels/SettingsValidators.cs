using System;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public static class SettingsValidators
    {

        public static bool IsUriValid(string apiAddress)
        {
            return Uri.TryCreate(
                apiAddress,
                UriKind.Absolute,
                out Uri convetedUri)
                && convetedUri != null && (
                    convetedUri.Scheme == Uri.UriSchemeHttp
                    || convetedUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}