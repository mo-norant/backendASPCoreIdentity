import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  
  isMaartenSlim : boolean= false;
  isMaartenKnap : boolean = true;
  naam : string  = "mo";
  leeftijd : number = 15;

  constructor(){
    
   this.naam = "francois";
    
  }

  private doIets(){
    alert();
  }

  
}
