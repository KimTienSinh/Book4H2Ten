using Book4H2Ten.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Book4H2Ten.Core.Enums.EnumLibrary;


namespace Book4H2Ten.EntityFrameWorkCore.Repositories
{
    public interface IBaseService<T> where T : BaseEntity
    {

    }

    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        public readonly IRepository<T> _repository;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly string? Fullname;
        public readonly string? Email;
        public readonly string? PhoneNumber;
        public readonly long? Id;
        public readonly Guid? UserId;
        public readonly UserGender? Gender;
        public readonly string? TimeZone;
        public readonly DateTime CurrentDate = DateTime.UtcNow;

        public BaseService(IRepository<T> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            //Fullname = GetClaimValue("Book4H2Ten:Fullname");
            /*PhoneNumber = GetClaimValue("Book4H2Ten:PhoneNumber");
            Email = GetClaimValue("Book4H2Ten:Email");
            Id = GetClaimValue("Book4H2Ten:Id") != null ? long.Parse(GetClaimValue("Book4H2Ten:Id")!) : null;
            UserId = GetClaimValue("Book4H2Ten:GuidId") != null ? Guid.Parse(GetClaimValue("Book4H2Ten:GuidId")!) : null;
            Gender = GetClaimValue("Book4H2Ten:Gender") != null ? Enum.Parse<UserGender>(GetClaimValue("Book4H2Ten:Gender")!) : null;
            TimeZone = GetKeyValueFromHeader("Timezone") ?? "Asia/SaiGon";*/
        }

        private string? GetClaimValue(string type)
        {
            return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(c => c.Type == type)?.Value;
        }

        private string GetKeyValueFromHeader(string key)
        {
            return _httpContextAccessor.HttpContext.Request.Headers[key]!;
        }


    }
}
