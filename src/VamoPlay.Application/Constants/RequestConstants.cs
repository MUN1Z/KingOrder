namespace VamoPlay.Application.Constants
{
    public static class RequestConstants
    {
        #region constants

        public const int Size10MB = 10;
        public const int Size50MB = 50;
        public const int Length50 = 50;
        public const int Length253 = 253;
        public const int Length255 = 255;
        public const int Length999 = 999;
        public const int Length1000 = 1000;
        public const int Length2048 = 2048;
        public const int Length10000 = 10000;
        public const int PortMin = 1;
        public const int PortMax = 65535;
        public const double MinNonNegativeValue = 0;
        public const double MinNonZeroValue = 1;
        public const double MaxDecimalValue = 999999999.99;
        public const int MinPositiveValue = 1;
        public const int MaxComponentQuantityValue = 999;
        public const double MaxAdditionMin = 0;
        public const double MaxAdditionMax = 9999.99d;
        public const int MaxAdditionMaxInteger = 9999;
        public const int MaxDiscountMin = 0;
        public const int MaxDiscountMax = 100;
        public const int MaxRequestSize50MBInBytes = 52428800;
        public const string Rfc3339DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffffffK";

        #endregion
    }
}
