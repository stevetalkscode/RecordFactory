// ReSharper disable UnusedParameter.Local
namespace RecordFactoryDemo
{
    public class UserAccessor
    {
        //public UserAccessor(IHttpContextAccessor ctx)
        //{
        // in a real-world scenario, this would take IHttpContextAccessor 
        // and get the details from HttpContext.User 
        // using Claims to get the user id for example.

        //var name = ctx.HttpContext.User.Identity?.Name;
        //}

        public IUser GetUser() => new User(1, "Steve");

        private record User(int UserId, string Name) : IUser;
    }
}