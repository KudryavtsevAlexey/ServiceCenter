using System.Threading.Tasks;

using KudryavtsevAlexey.ServiceCenter.Models;

namespace KudryavtsevAlexey.ServiceCenter.Data
{
    public interface IWrapper
    {
        Task<Device> FirstOrDefaultDeviceAsyncWrapper(int? id);
    }
}
