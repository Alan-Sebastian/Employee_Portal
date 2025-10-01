import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable, tap } from 'rxjs';
import { User } from '../Model/User';

import { ResponseData } from '../Model/Response';
import { TokenService } from './token.service';
import { LoginRequest } from '../Model/loginRequest';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {


  http=inject(HttpClient)

  tokenService=inject(TokenService)

  router=inject(Router)
  





login(credentials:LoginRequest): Observable<ResponseData> {
    

    const loginModel = new User(credentials.userName, credentials.password);
  
    return this.http.post<ResponseData>(
      `${environment.authApiUrl}/Login`,
      loginModel,
      { headers: { 'Content-Type': 'application/json' } }
    ).pipe(
      tap((response: ResponseData) => {

        if (response && response.jwtToken) {
          this.tokenService.setToken(response.jwtToken);
        }
      })
    );
  }
  
  signup(credentials:NgForm){

var userModel=new User(credentials.value.UserName,credentials.value.password);
this.http.post(`${environment.authApiUrl}/Register`,userModel).subscribe((data)=>{
  })
  }

  logout() {
    this.tokenService.removeToken();
    this.router.navigate(['/login']);
}
    
}

