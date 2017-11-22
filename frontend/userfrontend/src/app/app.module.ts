import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BodyComponent } from './body/body.component';


import { MatMenuModule, MatButtonModule} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatToolbarModule} from '@angular/material/toolbar';

import { RouterModule, Routes } from '@angular/router';
import { AuthService } from './services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { TopbarComponent } from './topbar/topbar.component';
import { LoginComponent } from './body/login/login.component';
import { RegisterComponent } from './body/register/register.component';
import { HomeComponent } from './body/home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';

@NgModule({
  declarations: [
    AppComponent,
    BodyComponent,
    TopbarComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    NotfoundComponent
  ],
  imports: [
    BrowserModule,
    MatMenuModule,
    MatButtonModule,
    RouterModule,
    BrowserAnimationsModule,
   HttpClientModule,
   MatToolbarModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
