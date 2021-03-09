using Microsoft.Azure.Cosmos.Table;

namespace shared.DTO
{
    public interface IEntityColumns
    {
        static string[] Columns { get; }
    }

    public class UserTableEntity : TableEntity, IEntityColumns
    {
        public UserTableEntity()
        {
        }

        public UserTableEntity(string _name, string _email)
        {
            PartitionKey = _name;
            RowKey = _email;
            name = _name;
            email = _email;
            
        }

        public static string[] Columns { get { return new string [] { "name", "email" }; } }

        public string name { get; set; }
        public string email { get; set; }
    }
}