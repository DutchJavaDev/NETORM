using NETORM.Data;  

namespace NETORM.Interface
{
    public interface ITransAction
    {
        Task CreateTable(TableRecord tableRecord);
    }
}
