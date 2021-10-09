using KudryavtsevAlexey.ServiceCenter.Data;

namespace KudryavtsevAlexey.ServiceCenter.Services
{
	public interface IJwtService
    {
        public string CreateToken(ApplicationUser user);
    }
}
