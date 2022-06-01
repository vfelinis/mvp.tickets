namespace mvp.tickets.domain.Helpers
{
    public static class ThrowHelper
    {
        public static T ArgumentNull<T>()
        {
            throw new ArgumentNullException(typeof(T).ToString());
        }

        public static void ArgumentNull(string name)
        {
            throw new ArgumentNullException(name);
        }
    }
}
