﻿using HSNXT.ComLib.Lang.Core;

namespace HSNXT.ComLib.Lang.Types
{
    /// <summary>
    /// Used to store a bool value.
    /// </summary>
    public class LNull : LObject
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public LNull()
        {
            this.Type = LTypes.Null;
        }
    }


    
    /// <summary>
    /// Class to represent null
    /// </summary>
    public class LNullType : LObjectType
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public LNullType()
        {
            this.Name = "null";
            this.FullName = "sys.null";
            this.TypeVal = TypeConstants.Null;
        }


        /// <summary>
        /// Sets up the matrix of possible conversions from one type to another type.
        /// </summary>
        public override void SetupConversionMatrix()
        {
            this.SetDefaultConversionMatrix(TypeConversionMode.NotSupported);
        }
    }
}
