using System.Data;
using Everis.ToolBox.Services;

namespace Everis.ToolBox.Extensions.CommandBuilders
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
