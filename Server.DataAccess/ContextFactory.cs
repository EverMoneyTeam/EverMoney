using Server.DataAccess.Context;

namespace Server.DataAccess
{
    public static class ContextFactory
    {
        private static SecurityContext _securityContext;

        public static SecurityContext SecurityContext => _securityContext ?? (_securityContext = new SecurityContext());
    }
}
