namespace JaySharp.Testing;

/// <summary>
/// Provides a lightweight, thread-safe in-memory CRUD repository for testing database-backed service logic without live DB calls.
/// </summary>
/// <typeparam name="TKey">The unique key type.</typeparam>
/// <typeparam name="TEntity">The entity object type.</typeparam>
public class FakeRepository<TKey, TEntity> where TKey : notnull
{
    private readonly Dictionary<TKey, TEntity> _store = new();
    private readonly object _lock = new();

    /// <summary>
    /// Gets the count of entities stored in the repository.
    /// </summary>
    public int Count
    {
        get
        {
            lock (_lock)
            {
                return _store.Count;
            }
        }
    }

    /// <summary>
    /// Adds or updates an entity in the repository.
    /// </summary>
    /// <param name="key">The primary key identifier.</param>
    /// <param name="entity">The entity instance.</param>
    public void Add(TKey key, TEntity entity)
    {
        lock (_lock)
        {
            _store[key] = entity;
        }
    }

    /// <summary>
    /// Retrieves an entity by key, or returns default if not found.
    /// </summary>
    /// <param name="key">The primary key identifier.</param>
    /// <returns>The entity if found; otherwise, default.</returns>
    public TEntity? Get(TKey key)
    {
        lock (_lock)
        {
            return _store.TryGetValue(key, out TEntity? entity) ? entity : default;
        }
    }

    /// <summary>
    /// Retrieves all entities currently stored in the repository.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public List<TEntity> GetAll()
    {
        lock (_lock)
        {
            return _store.Values.ToList();
        }
    }

    /// <summary>
    /// Removes an entity from the repository by key.
    /// </summary>
    /// <param name="key">The primary key identifier.</param>
    /// <returns><c>true</c> if removed; otherwise, <c>false</c>.</returns>
    public bool Delete(TKey key)
    {
        lock (_lock)
        {
            return _store.Remove(key);
        }
    }

    /// <summary>
    /// Clears all entities from the repository.
    /// </summary>
    public void Clear()
    {
        lock (_lock)
        {
            _store.Clear();
        }
    }
}
