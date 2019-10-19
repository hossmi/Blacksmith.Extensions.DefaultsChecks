﻿using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ToolBox.Extensions.OleDbCommands
{
    public static class OleDBCommandExtensions
    {
        public static OleDbCommand setParameter(this OleDbCommand command, string name, object value, OleDbType type)
        {
            OleDbParameter commandParameter = command.Parameters.Add(name, type);
            commandParameter.Value = value;
            //commandParameter.DbType = DbType.Decimal;

            return command;
        }
    }
}
