import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../utils/constants';
import { Http, Headers, Response, RequestOptions,  } from '@angular/http';
import {Router} from "@angular/router";

@Injectable()
export class AuthService {

  constructor(private http: HttpClient, private router : Router) {
     
   }



  public getToken(){
    
      var tokenstring  = localStorage.getItem('tokenUser');
      if(tokenstring != null){
        var token = JSON.parse(tokenstring); 
        console.log(token["access_token"])
        return true;
      }

      return false;

    
  }

  private setToken(){
    
  }


  public login(username: string, password: string){

    var headers = new HttpHeaders();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    
    let body = new URLSearchParams();
    body.set('username', username);
    body.set('password', password);
    body.set('scope', Constants.SCOPE);
    body.set('grant_type', Constants.GRANT_TYPE);
    body.set('client_id', Constants.CLIENT_ID);
    
    let options = {
        headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };

 
     this.http.post(Constants.GET_TOKEN, body.toString(), options).subscribe(data => {

      if(data["access_token"]){
        data["life"] = Date + data['expires_in'];
        localStorage.setItem('tokenUser', JSON.stringify(data));
        this.router.navigate(['dashboard']);
      }
    });

  }

  public logout(): void {

  }
}
