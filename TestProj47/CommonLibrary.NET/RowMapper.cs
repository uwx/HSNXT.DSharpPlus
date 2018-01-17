﻿#if NetFX
using System;
using HSNXT.ComLib.Data;

namespace HSNXT.ComLib.Entities
{
    /// <summary>
    /// Maps a row of data from a IDataReader to the entity of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityRowMapper<T> : RowMapperReaderBased<T>, IEntityRowMapper<T>
    {
        /// <summary>
        /// Allows derived classes to build instances of the entity.
        /// </summary>
        protected Func<object, T> _entityFactoryMethod;
    }
}
#endif