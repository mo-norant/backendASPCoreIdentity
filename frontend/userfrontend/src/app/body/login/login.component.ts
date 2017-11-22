import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

error


  constructor(public auth : AuthService) { }

  ngOnInit() {
  }

  public login(){
    this.auth.login("info@norant.be","Admin01*").subscribe(
      data => {
        console.log(data)
      },
      error => {
        console.log(error)
        //this.error = error.ToString();
      },
      () => {
        console.log("I'm done subscribing to this request.");
      }
    );
  }

}
