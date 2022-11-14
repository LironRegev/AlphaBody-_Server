using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DBConverters
{
    interface IDBConverter<TDBType, TDataType>
    {
        TDBType ToDBType(TDataType dataType);
        TDataType ToDataType(TDBType dbType);
    }
}
