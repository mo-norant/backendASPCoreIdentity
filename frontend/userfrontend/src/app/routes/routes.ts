import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../body/home/home.component';
import { LoginComponent } from '../body/login/login.component';
import { RegisterComponent } from '../body/register/register.component';
import { NotfoundComponent } from '../notfound/notfound.component';
import { DashboardComponent } from '../dashboard/dashboard.component';



const appRoutes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: '**', component: NotfoundComponent }
 
  
  
  ];
  

  export const routing = RouterModule.forRoot(appRoutes);