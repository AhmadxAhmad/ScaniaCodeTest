import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/enviroments/environment';

@Injectable({
  providedIn: 'root',
})
export class SimulatorService {

  private readonly apiUri: string ;
  constructor(private _httpClient: HttpClient) {
    this.apiUri=environment.Simulator.api.url+'VehicleSimulator';
  }


  async update(): Promise<any> {
    try {
       await this._httpClient.get(this.apiUri).toPromise();
    } catch (error) {

      console.error('Error fetching update:', error);
      throw error;
    }
  }

}
