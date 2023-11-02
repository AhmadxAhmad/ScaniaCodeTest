import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorComponent } from './shared/error-component/error-component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { VehiclesComponent } from './pages/vehicles/vehicles.component';
import { CustomersComponent } from './pages/customers/customers.component';

const routes: Routes = [ 
  { path: '', redirectTo: '/vehicles', pathMatch: 'full' },
  { path: 'customers', component: CustomersComponent },

  { path: 'vehicles', component: VehiclesComponent },
  { path: '**', component: ErrorComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes
    // ,{ useHash: true }
    )],
  exports: [RouterModule]
})
export class AppRoutingModule { }
