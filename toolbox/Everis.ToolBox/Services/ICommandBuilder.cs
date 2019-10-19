using System.Data;

namespace Everis.ToolBox.Services
{
    public interface ICommandBuilder
    {
        ICommandBuilder setQuery(string query);
        ICommandBuilder setParameter(string name, object value);
        IDbCommand build();
    }
}