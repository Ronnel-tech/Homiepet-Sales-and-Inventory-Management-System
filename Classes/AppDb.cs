using System;
using System.Configuration;

namespace red_framework
{
    /// <summary>
    /// Centralized database provider. All forms should use AppDb.Instance.
    /// </summary>
    public static class AppDb
    {
        private static readonly Lazy<IDatabase> _instance = new Lazy<IDatabase>(() =>
        {
            string connStr = ConfigurationManager.ConnectionStrings["AppDB"].ConnectionString;
            return new SqlDatabase(connStr);
        });

        /// <summary>
        /// The shared database instance used across the app.
        /// </summary>
        public static IDatabase Instance => _instance.Value;

        /// <summary>
        /// Call this on app exit to clean up.
        /// </summary>
        public static void Dispose()
        {
            if (_instance.IsValueCreated)
                _instance.Value.Dispose();
        }
    }
}