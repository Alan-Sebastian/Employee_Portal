import { Component, inject } from '@angular/core';
import { AuthService } from 'src/Main/Shared/Services/auth.service';
import { TokenService } from 'src/Main/Shared/Services/token.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  authService=inject(AuthService);
  tokenService=inject(TokenService)

   logout() {
    this.authService.logout(); // clears sessionStorage and navigates to login
  }
  isLoggedIn(): boolean {
    return !!this.tokenService.getToken();
  }

}
