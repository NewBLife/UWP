using System.Threading.Tasks;

namespace SuspensionManagerDemo.Model
{
    public interface IDataService
    {
        Task<DataItem> GetData();
    }
}