import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../utils/constants';
import { Http, Headers, Response, RequestOptions,  } from '@angular/http';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient) {

    

   }



  public getToken(){

  }

  private setToken(){
    
  }


  public private(username: string, password: string){

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

 
   
    return this.http.post(Constants.GET_TOKEN, body.toString(), options)

  }

  public logout(): void {

  }
}
