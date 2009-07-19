using System;
using System.Security.Principal;

namespace AveImperator.Library.Security
{
    [Serializable()]
    public class AIPrincipal : Csla.Security.BusinessPrincipalBase
    {
        private AIPrincipal( IIdentity identity ) : base( identity ) { }

        public static bool Login( long facebookId )
        {
            AIIdentity identity = AIIdentity.GetIdentity( facebookId );
            if ( identity.IsAuthenticated )
            {
                AIPrincipal principal = new AIPrincipal( identity );
                Csla.ApplicationContext.User = principal;
            }
            return identity.IsAuthenticated;
        }

        public static void Register( long facebookId, string facebookName, Gladiator gladiator )
        {
            AIIdentity.Register( facebookId, facebookName, gladiator );
        }

        public static void Logout()
        {
            AIIdentity identity = AIIdentity.UnauthenticatedIdentity();
            AIPrincipal principal = new AIPrincipal( identity );
            Csla.ApplicationContext.User = principal;
        }

        public override bool IsInRole( string role )
        {
            AIIdentity identity = (AIIdentity)this.Identity;
            return identity.IsInRole( role );
        }
    }
}
