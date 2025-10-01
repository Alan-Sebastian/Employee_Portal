import { Component, inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/Main/Shared/Model/loginRequest';
import { ResponseData } from 'src/Main/Shared/Model/Response';
import { AuthService } from 'src/Main/Shared/Services/auth.service';
import { TokenService } from 'src/Main/Shared/Services/token.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {


  authService=inject(AuthService);
  
  tokenService=inject(TokenService)


  private router = inject(Router);


  isLoginMode!:boolean;



  switchMode(){
    this.isLoginMode=!this.isLoginMode
  }
  submitDetails(credentials:NgForm){


    if(credentials.valid && this.isLoginMode)
    {
      // this.authService.login(credentials);
      const loginData: LoginRequest = credentials.value; 

    
      this.authService.login(loginData).subscribe({
        next: (response:ResponseData<any>) => {

    this.router.navigate(['/dashboard']); 

      this.tokenService.setToken(response.jwtToken);

  },
  error: err => {
    console.error('Login failed', err);
  }
      })
    }
    
    if(credentials.valid && !this.isLoginMode)
    {
      this.authService.signup(credentials)

    }

    
    

  }

}
