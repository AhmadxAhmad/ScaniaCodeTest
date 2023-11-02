import { Component } from '@angular/core';
import { CustomerViewModel } from 'src/app/ViewModels/customerViewModel';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent {
  customers:CustomerViewModel[];
constructor(private _customerService:CustomerService)
{
  
}
  async ngOnInit(){
    this.customers=(await this._customerService.getCustomers())
    

}
}
