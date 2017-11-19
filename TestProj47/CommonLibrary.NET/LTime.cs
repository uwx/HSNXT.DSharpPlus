﻿using System;
using HSNXT.ComLib.Lang.Core;

namespace HSNXT.ComLib.Lang.Types
{
    /// <summary>
    /// Used to store a timespan value.
    /// </summary>
    public class LTime : LObject
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="val"></param>
        public LTime(TimeSpan val)
        {
            this.Value = val;
            this.Type = LTypes.Time;
        }


        /// <summary>
        /// The raw type value.
        /// </summary>
        public TimeSpan Value;


        /// <summary>
        /// Gets the value of this object.
        /// </summary>
        /// <returns></returns>
        public override object GetValue()
        {
            return this.Value;
        }


        /// <summary>
        /// Clones this value.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new LTime(this.Value);
        }
    }



    /// <summary>
    /// Array type.
    /// </summary>
    public class LTimeType : LObjectType
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        public LTimeType()
        {
            this.Name = "time";
            this.FullName = "sys.time";
            this.TypeVal = TypeConstants.Time;
            this.IsSystemType = true;
        }


        /// <summary>
        /// Sets up the matrix of possible conversions from one type to another type.
        /// </summary>
        public override void SetupConversionMatrix()
        {
            this.AddConversionTo(TypeConstants.Array,     TypeConversionMode.NotSupported);
            this.AddConversionTo(TypeConstants.Bool,      TypeConversionMode.NotSupported);
            this.AddConversionTo(TypeConstants.Date,      TypeConversionMode.Supported);
            this.AddConversionTo(TypeConstants.Map,       TypeConversionMode.NotSupported);
            this.AddConversionTo(TypeConstants.Number,    TypeConversionMode.Supported);
            this.AddConversionTo(TypeConstants.Null,      TypeConversionMode.Supported);
            this.AddConversionTo(TypeConstants.String,    TypeConversionMode.Supported);
            this.AddConversionTo(TypeConstants.Time,      TypeConversionMode.SameType);
        }
    }
}
