using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace ApiNetTransportes.Helpers
{
    public class HelperToken
    {
        public String Issuer { get; set; }
        public String Audience { get; set; }
        public String SecretKey { get; set; }

        public HelperToken(IConfiguration configuration) {
            this.Issuer = configuration["ApiAuth:Issuer"];
            this.Audience = configuration["ApiAuth:Audience"];
            this.SecretKey = configuration["ApiAuth:SecretKey"];
        }
        //Método privado para generar una clave
        //simétrica a partir de nuestro secretkey
        public SymmetricSecurityKey GetKeyToken()
        {
            byte[] data = Encoding.UTF8.GetBytes(this.SecretKey);
            return new SymmetricSecurityKey(data);
        }
        //Método para configurar las opciones de seguridad del token
        //los métodos de configuración son action
        public Action<JwtBearerOptions> GetJwtOptions()
        {
            Action<JwtBearerOptions> jwtoptions =
                new Action<JwtBearerOptions>(options =>
                {
                    options.TokenValidationParameters =
                    new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.Issuer,
                        ValidAudience = this.Audience,
                        IssuerSigningKey = this.GetKeyToken()
                    };
                });
            return jwtoptions;
        }
        //Método para validar la aunteticación 
        public Action<AuthenticationOptions> GetAuthenticationOptions()
        {
            Action<AuthenticationOptions> authoptions =
                new Action<AuthenticationOptions>(options =>
                {
                    options.DefaultScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                });
            return authoptions;
        }

     
    }
}