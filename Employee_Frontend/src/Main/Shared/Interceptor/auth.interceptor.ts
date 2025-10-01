import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from '../Services/token.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private tokenService:TokenService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {



  
if (request.url === `${environment.authApiUrl}/Login` ||
    request.url === `${environment.authApiUrl}/Register`
    ) {
      return next.handle(request);
    }


    const token = this.tokenService.getToken();

  if (!token) {
    // No token -> redirect to login
    this.router.navigate(['/login']);
    // Return empty observable to cancel the request
    return new Observable<HttpEvent<any>>();
  }

  const cloned = request.clone({
    headers: request.headers.set('Authorization', `Bearer ${token}`)
  });

  debugger;
  return next.handle(cloned);


  }
}
