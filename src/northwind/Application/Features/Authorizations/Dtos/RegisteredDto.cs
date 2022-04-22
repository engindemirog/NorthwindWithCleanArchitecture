using Core.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authorizations.Dtos
{
    public class RegisteredDto
    {
        public AccessToken AccessToken { get; set; }
    }
}
