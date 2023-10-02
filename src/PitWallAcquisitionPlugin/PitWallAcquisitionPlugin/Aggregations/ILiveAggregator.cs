using FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations;

namespace PitWallAcquisitionPlugin.Aggregations
{
    public interface ILiveAggregator
    {
        bool IsDirty { get; }

        /// <summary>
        /// Receives session time left from simhub runtime.
        /// </summary>
        /// <param name="sessionTimeLeft"></param>
        void AddSessionTimeLeft(string sessionTimeLeft);

        /// <summary>
        /// Adds the pilot name to api
        /// </summary>
        /// <param name="original"></param>
        void AddPilotName(string original);

        /// <summary>
        /// <para>Add laptime as string to be parsed.</para>
        /// 
        /// <para>Format is <i>HH:MM:SS.sssssss</i></para>
        /// </summary>
        /// <param name="original"></param>
        void AddLaptime(string original);

        /// <summary>
        /// <para>The front left tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void AddFrontLeftTyreWear(double tyreWearValue);

        /// <summary>
        /// <para>The front right tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void AddFrontRightTyreWear(double tyreWearValue);

        /// <summary>
        /// <para>The rear left tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void AddRearLeftTyreWear(double tyreWearValue);

        /// <summary>
        /// <para>The rear right tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void AddRearRightTyreWear(double tyreWearValue);

        void Clear();

        /// <summary>
        /// The data object to be send to the API
        /// </summary>
        /// <returns></returns>
        IData AsData();
    }
}