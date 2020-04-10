using System;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace AzureRedisCache
{
    class Program
    {
        static void Main(string[] args)
        {

            IDatabase  cache = Connection.GetDatabase();
             var key = "ExamCode";
             cache.StringSet(key,"Az203");
             cache.KeyExpire(key,new TimeSpan(0,0,30));

             var customerKey ="CurrentCustomer";
             var customer = new Customer(1,"James");
             cache.StringSet(customerKey, JsonConvert.SerializeObject(customer));

             var fromCache = JsonConvert.DeserializeObject<Customer>(cache.StringGet(customerKey));

             Console.Write(fromCache.Name);
            Connection.Dispose();
             

            Console.Read();
        }

        private static Lazy<ConnectionMultiplexer> redisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string connectionString = "az203mukur.redis.cache.windows.net:6380,password=IRC3Wr2BTOVSgwRqmGSOv3I67TEJ9eEJeGyeNevotPs=,ssl=True,abortConnect=False";
            return ConnectionMultiplexer.Connect(connectionString);
        });

         public static ConnectionMultiplexer Connection
        {
            get
            {
                return redisConnection.Value;
            }
        }
    }


}
