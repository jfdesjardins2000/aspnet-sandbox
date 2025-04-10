import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from '../services/auth.service';
import { UserModel } from '../models/user.model';
import { jwtDecode } from "jwt-decode";


export const authGuard: CanActivateFn = (route, state) => {
  //return true;
  const cookieService = inject(CookieService);
  const authService = inject(AuthService);
  const router = inject(Router);
  const user = authService.getUser();
  
  // Check if the user is logged in by checking the cookie
  const token = cookieService.get('Authorization');
  console.log('Auth Cookie:', token); // Debugging line

  if (token && user)  {
    // Check if the token (JWT) is in the correct format
    const jwtToken = token.replace('Bearer ', '');
    const decodedToken: any = jwtDecode(jwtToken);
    console.log('Decoded Token:', decodedToken); // Debugging line
    
    // Check if token has expired
    // decodedToken.exp is in seconds, so we need to multiply by 1000 to convert to milliseconds
    // and compare with the current time in milliseconds
    const expirationDate = decodedToken.exp * 1000;
    const currentTime = new Date().getTime();
    const isValidToken:boolean = currentTime < expirationDate;
    console.log('Is valid token:', isValidToken); // Debugging line

    if (isValidToken) {
      // Token is still valid
      if (user.roles.includes('Writer')) {
        return true; // Allow access to the route
      } else {
        alert('Unauthorized');
        return false;
      }
    } else {
      console.error('Invalid token format'); // Debugging line
      authService.logout(); // Log out the user if the token is invalid
      return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } })
    }
    
  } else {
    console.error('User is not logged in'); // Debugging line
    authService.logout(); // Log out the user if not logged in
    return router.createUrlTree(['/login'], { queryParams: { returnUrl: state.url } })
  }
};
