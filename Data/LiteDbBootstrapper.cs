using FUPlaner.Entities;
using LiteDB;

namespace FUPlaner.Data
{
  public static class LiteDbBootstrapper
  {
    private static string _dbPath;
    public static LiteDatabase Initialize(string dbPath = null)
    {
      BsonMapper.Global.Entity<Lesson>();
      
      _dbPath = dbPath ?? @"../db/FUPlaner.db";
      return new LiteDatabase(_dbPath);
    }

    public static string GetDatabasePath()
    {
      return _dbPath;
    }
  }
}