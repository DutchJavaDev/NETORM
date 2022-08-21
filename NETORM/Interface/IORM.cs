using NETORM.Data;  

namespace NETORM.Interface
{
    public interface IORM
    {
        Task CreatDatabase();
        Task CreateTable(TableRecord tableRecord);
    }
}
