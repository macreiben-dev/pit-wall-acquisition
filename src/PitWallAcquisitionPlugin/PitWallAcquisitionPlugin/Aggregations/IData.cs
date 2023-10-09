using System;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public interface IData
    {
        double? LaptimeSeconds { get; }
     
        string PilotName { get; }
        
        string SessionTimeLeft { get; }
        
        ITyresWear TyresWear { get; }

        ITyresTemperatures TyresTemperatures { get; }
    }
}