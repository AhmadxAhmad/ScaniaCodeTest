namespace ScaniaTest.Common.Entities

{

        public class Vehicle 
        {
            public string Id { get; set; }
            public string RegNr { get; set; }
            public int CustomerId { get; set; }
            public bool Status { get; set; }
            public Customer Customer {get;set;}
        }
  
}