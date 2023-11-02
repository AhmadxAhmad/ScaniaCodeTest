namespace ScaniaTest.Common.Entities
{
 
        public class Customer
        {
            public int Id {get;set;}
            public string Name {get; set;}
            public string Address {get;set;}
            public IEnumerable<Vehicle> Vehicles {set; get;}
            
        }        

}