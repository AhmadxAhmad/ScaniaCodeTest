import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/enviroments/environment';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {

  private readonly apiUri: string ;
  constructor(private _httpClient: HttpClient) {
    this.apiUri=environment.Vehicle.api.url+'Vehicle';
  }

  async getVehicles(): Promise<any> {
    try {
      const response = await this._httpClient.get<any>(this.apiUri).toPromise();
      return response;
    } catch (error) {
        return null;
    }
  }
  
  async GetAllByStatus(status:boolean){
    try {
      const response = await this._httpClient.get<any>(this.apiUri+"/ByStatus/"+status).toPromise();
      return response;
    } catch (error) {
      return null;
    }
  }

  async GetAllByCustomerId(id:number): Promise<any> {
    try {
      const response = await this._httpClient.get<any>(this.apiUri+"/ByCustomer/"+id).toPromise();
      return response;
    } catch (error) {
      console.error('Error fetching vehicles:', error);
      return null;
    }
  }

}
