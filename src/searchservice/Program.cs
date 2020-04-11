using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;

namespace searchservice
{
    class Customer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }
        [IsFilterable]
        public string Name { get; set; }
        [IsFilterable, IsSortable, IsFacetable]
        public string Course { get; set; }
        [IsSearchable]
        public string Comment { get; set; }

        [IsSortable]
        public string Progress { get; set; }
    }
    class Program
    {
        static string searchServiceName = "az203bira";
        static string adminApiKey = "AE7855FDC9210F1A35D8807B85B697D6";
        static void Main(string[] args)
        {
            SearchServiceClient democlient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            //CreateIndex("customers",democlient);
            UploadIndexData("customers", democlient);
            Console.Read();
        }

        static void CreateIndex(string indexName, SearchServiceClient client)
        {
            var index = new Microsoft.Azure.Search.Models.Index()
            {
                Name= indexName,
                Fields = FieldBuilder.BuildForType<Customer>()
            };
            client.Indexes.Create(index);
            Console.WriteLine($"index created :{indexName}");
        }
        static void UploadIndexData(string indexName, SearchServiceClient client)
        {
            var customers = new Customer[]{
                new Customer(){
                    Id="11",
                    Name="James",
                    Progress="80",
                    Comment="You got this",
                    Course="Az203 Developing Azure Solutions"
                },
                  new Customer(){
                    Id="22",
                    Name="John",
                    Progress="60",
                    Comment="You got this",
                    Course="Az203 Developing Azure Solutions"
                }

            };


          var batch = IndexBatch.Upload(customers);
          ISearchIndexClient indexClient = client.Indexes.GetClient(indexName);
          
          try{
              indexClient.Documents.Index(batch);
              Console.WriteLine($"Inserted two docs");
          }
          catch(IndexBatchException ex){
              Console.WriteLine(ex.Message);
          }

        }
    }
}
