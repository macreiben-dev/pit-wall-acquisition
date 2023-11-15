namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface ILiveAggregator
    {
        bool IsDirty { get; }

        /// <summary>
        /// Receives session time left from simhub runtime.
        /// </summary>
        /// <param name="sessionTimeLeft"></param>
        void SetSessionTimeLeft(string sessionTimeLeft);

        /// <summary>
        /// <para>Add laptime as string to be parsed.</para>
        /// 
        /// <para>Format is <i>HH:MM:SS.sssssss</i></para>
        /// </summary>
        /// <param name="original"></param>
        void SetLaptime(string original);

        /// <summary>
        /// <para>The front left tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void SetFrontLeftTyreWear(double? tyreWearValue);

        /// <summary>
        /// <para>The front right tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void SetFrontRightTyreWear(double? tyreWearValue);

        /// <summary>
        /// <para>The rear left tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void SetRearLeftTyreWear(double? tyreWearValue);

        /// <summary>
        /// <para>The rear right tyre wear.</para>
        ///                 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreWearValue"></param>
        void SetRearRightTyreWear(double? tyreWearValue);

        // ====================================================

        /// <summary>
        /// <para>Front left tyre temp.</para> 
        /// 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreTempValue"></param>
        void SetFrontLeftTyreTemperature(double? tyreTempValue);

        /// <summary>
        /// <para>Front right tyre temp.</para> 
        /// 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreTempValue"></param>
        void SetFrontRightTyreTemperature(double? tyreTempValue);

        /// <summary>
        /// <para>Rear left tyre temp.</para> 
        /// 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreTempValue"></param>
        void SetRearLeftTyreTemperature(double? tyreTempValue);

        /// <summary>
        /// <para>Rear right tyre temp.</para> 
        /// 
        /// <para>Format is xxx.xxxxxxxxxxxxx</para>
        /// </summary>
        /// <param name="tyreTempValue"></param>
        void SetRearRightTyreTemperature(double? tyreTempValue);

        /// <summary>
        /// <para>Set the average road wetness</para>
        /// </summary>
        /// <param name="data"></param>
        void SetAvgWetness(double? data);

        /// <summary>
        /// Sets the air temperature
        /// </summary>
        /// <param name="data"></param>
        void SetAirTemperature(double? data);

        // ====================================================

        /// <summary>
        /// Clears the setted data.
        /// </summary>
        void Clear();

        /// <summary>
        /// The data object to be send to the API
        /// </summary>
        /// <returns></returns>
        IData AsData();
       
    }
}