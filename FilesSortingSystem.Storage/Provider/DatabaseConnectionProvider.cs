using System.Diagnostics;
using SQLite;

namespace FilesSortingSystem.Storage.Provider
{
    public class DatabaseConnectionProvider
    {
        private SQLiteAsyncConnection? _connection;
        private readonly SemaphoreSlim _connectionLock = new(1, 1);

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            await _connectionLock.WaitAsync();
            try
            {
                if (_connection != null)
                    return _connection;

                var databasePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                    "FileSortingSystem.db"
                    );
//#if DEBUG
//                if(File.Exists(databasePath))
//                    File.Delete(databasePath);
//#endif
                _connection = new SQLiteAsyncConnection(databasePath);

                return _connection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DatabaseConnectionProvider] Error creating database connection: {ex}");
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }
}