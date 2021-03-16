﻿namespace Car.Data
{
    public static class Constants
    {
#pragma warning disable SA1310 // Field names should not contain underscore
        public const int ID_LENGTH = 0;
        public const int STRING_MAX_LENGTH = 64;

        public const int EMAIL_MIN_LENGTH = 2;
        public const int EMAIL_MAX_LENGTH = 100;

        public const int JSON_MIN_LENGTH = 2;
        public const int JSON_MAX_LENGTH = 100;

        public const int POSITION_MAX_LENGTH = 2;
        public const int LOCATION_MAX_LENGTH = 100;

        public const int PLATENUMBER_MIN_LENGTH = 4;
        public const int PLATENUMBER_MAX_LENGTH = 10;

        public const int SEATS_MAX_LENGTH = 8;

        public const int COMMENTS_MAX_LENGTH = 100;

        public const int TEXT_MAX_LENGTH = 500;
#pragma warning restore SA1310 // Field names should not contain underscore

    }
}