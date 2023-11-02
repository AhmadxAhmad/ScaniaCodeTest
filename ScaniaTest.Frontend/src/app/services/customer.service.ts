import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/enviroments/environment';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {

  private readonly apiUri: string ;
  constructor(private _httpClient: HttpClient) {
    this.apiUri=environment.Customer.api.url+'customer';
  }


  async getCustomers(): Promise<any> {
    try {
      const response = await this._httpClient.get<any>(this.apiUri).toPromise();
      return response;
    } catch (error) {
      console.error('Error fetching vehicles:', error);
      throw error;
    }
  }

}
