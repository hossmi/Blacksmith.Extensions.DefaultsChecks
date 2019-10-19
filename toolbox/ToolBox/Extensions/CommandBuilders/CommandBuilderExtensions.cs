using System.Collections.Generic;
using System.Data;
using ToolBox.Services;

namespace ToolBox.Extensions.CommandBuilders
{
    public static class CommandBuilderExtensions
    {
        public static T build<T>(this ICommandBuilder commandBuilder) where T: class, IDbCommand
        {
            IDbCommand command = commandBuilder.build();
            return command as T;
        }
    }
}
