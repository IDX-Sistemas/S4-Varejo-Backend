using  System;
using System.Data;

namespace IdxSistemas.AppRepository.Utils
{
    
    public static class SQLUtils
    {
         public static T GetValue<T>(IDataReader DataReader, string FieldName)
        {
            int FieldIndex;
            try
            {
                FieldIndex = DataReader.GetOrdinal(FieldName);
            }
            catch
            {
                return default(T);
            }

            if (DataReader.IsDBNull(FieldIndex))
            {
                return default(T);
            }
            else
            {
                object readData = DataReader.GetValue(FieldIndex);
                if (readData is T)
                {
                    return (T)readData;
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(readData, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
            }
        }
    }
}