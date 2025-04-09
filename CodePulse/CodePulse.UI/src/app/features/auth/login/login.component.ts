// login.component.ts
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
// import { LoginRequestModel  } from ''./models/login-request.model"; 
import { LoginRequestModel } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  constructor(private authService: AuthService,
    private cookieService: CookieService,
    private router: Router) {}

  onFormSubmit() {
    if (this.loginForm.valid) {
      // Créer un objet du type LoginRequestModel avec les valeurs du formulaire
      const loginRequest: LoginRequestModel = {
        email: this.loginForm.get('email')?.value || '',
        password: this.loginForm.get('password')?.value || ''
      };
      
      // Afficher les valeurs dans la console
      console.log('LoginRequestModel:', loginRequest);
      
      // Plus tard, vous pourrez utiliser cet objet pour l'envoi au backend
      // authService.login(loginRequest).subscribe(...)
      this.authService.login(loginRequest)
      .subscribe({
        next: (response) => {
          // Set Auth Cookie
          this.cookieService.set('Authorization', `Bearer ${response.token}`,
          undefined, '/', undefined, true, 'Strict');
  
          // Set User
          this.authService.setUser({
            email: response.email,
            roles: response.roles
          });
  
          // Redirect back to Home
          this.router.navigateByUrl('/');
  
        }
      });




    } else {
      this.loginForm.markAllAsTouched();
      console.log('Formulaire invalide');
    }
  }
}