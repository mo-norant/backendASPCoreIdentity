import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { BodyComponent } from './body/body.component';
import { RegisterComponent } from './body/register/register.component';
import { LoginComponent } from './body/login/login.component';

import { MatMenuModule, MatButtonModule} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { RouterModule, Routes } from '@angular/router';
import { AuthService } from './services/auth.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    BodyComponent,
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    MatMenuModule,
    MatButtonModule,
    RouterModule,
    BrowserAnimationsModule,
   HttpClientModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
