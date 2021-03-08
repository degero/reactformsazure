using Microsoft.Azure.Cosmos.Table;

public class UserEntity : TableEntity
{
    public UserEntity()
    {
    }

    public UserEntity(string name, string email)
    {
        PartitionKey = name;
        RowKey = email;
    }

    public string name { get; set; }
    public string email { get; set; }
}