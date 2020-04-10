using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FUPlaner.Entities;
using LiteDB;

namespace FUPlaner.Data
{
  public interface IData
  {
    Data<Lesson> Lessons { get; }
    Data<Plan> Plans { get; }
  }

  public class LiteDbData : IData
  {
    private readonly LiteDatabase _database;
    public Data<Lesson> Lessons => new Data<Lesson>(_database);
    public Data<Plan> Plans => new Data<Plan>(_database);

    public LiteDbData(LiteDatabase database)
    {
      _database = database;
    }
  }

  public class Data<T>
  {
    private readonly ILiteCollection<T> _entities;
    public Data(LiteDatabase database)
    {
      _entities = database.GetCollection<T>();
    }
    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, int skip = 0, int limit = 2147483647)
    {
      return _entities.Find(predicate, skip, limit);
    }
    public IEnumerable<T> FindAll()
    {
      return _entities.FindAll();
    }
    public T FindById(int id)
    {
      return _entities.FindById(new BsonValue(id));
    }
    public T FindOne(Expression<Func<T, bool>> predicate)
    {
      return _entities.FindOne(predicate);
    }
    public bool Save(T entity)
    {
      return _entities.Upsert(entity);
    }
    public bool Delete(int id)
    {
      return _entities.Delete(new BsonValue(id));
    }
  }
}