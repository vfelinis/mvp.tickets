namespace mvp.tickets.domain.Helpers
{
    public static class ThrowHelper
    {
        public static T NullArgument<T>()
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }
    }
}
