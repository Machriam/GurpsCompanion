using GurpsCompanion.Shared.Features.Authentication;

namespace GurpsCompanion.Server.Core
{
    public interface IAuthenticationVerifier
    {
        bool AuthenticationIsGranted(GlobalPasswordHashModel model);
    }

    public class AuthenticationVerifier : IAuthenticationVerifier
    {
        private readonly IPasswordCryptologizer _passwordCryptologizer;
        private readonly IEnvironmentConfiguration _configuration;

        public AuthenticationVerifier(IEnvironmentConfiguration configuration, IPasswordCryptologizer passwordCryptologizer)
        {
            _configuration = configuration;
            _passwordCryptologizer = passwordCryptologizer;
        }

        public bool AuthenticationIsGranted(GlobalPasswordHashModel model)
        {
            return _passwordCryptologizer.EncryptString(_configuration.GameMasterPasswordHash + model.Salt) == model.Hash;
        }
    }
}
