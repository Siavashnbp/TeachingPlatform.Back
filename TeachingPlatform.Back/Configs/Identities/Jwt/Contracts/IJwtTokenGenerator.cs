using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachingPlatform.Back.Configs.Identities.Jwt.Contracts
{
    public interface IJwtTokenGenerator
    {
        public string Generate(string userId, string role);
    }
}
