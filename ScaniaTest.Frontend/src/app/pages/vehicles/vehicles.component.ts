import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { VehicleViewModel } from 'src/app/ViewModels/vehicleViewModel';
import { SimulatorService } from 'src/app/services/simulator.service ';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.css']
})
export class VehiclesComponent {

  vehicles:VehicleViewModel[];

constructor(private _vehicleService:VehicleService,
            private _simulatorService:SimulatorService,
            private _router:Router)
{
  
}
 
async ngOnInit(){
   await this.GetAll();
}

async randomize(){
  await this._simulatorService.update()
  await this.GetAll();
}

formData = {
  id: 0
};

async GetAll(){
  this.vehicles=(await this._vehicleService.getVehicles())
}

async GetAllWithStatus(status:boolean){
  this.vehicles=(await this._vehicleService.GetAllByStatus(status))
}


async GetByCustomerId(){
  if(this.formData.id!=0){
    this.vehicles= await this._vehicleService.GetAllByCustomerId(this.formData.id);
  }
}

}
