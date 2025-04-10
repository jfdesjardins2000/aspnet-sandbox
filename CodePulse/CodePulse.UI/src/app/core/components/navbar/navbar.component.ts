import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../features/auth/services/auth.service';
import { UserModel } from '../../../features/auth/models/user.model';
import { response } from 'express';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {

  user?: UserModel;
  
  constructor(private authService:AuthService,
    private router: Router) {}

  ngOnInit(): void {
    this.authService.user()
    .subscribe({
      next: (response) => {
        console.log(response)
        this.user = response;
      }
    });

    this.user = this.authService.getUser();
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigateByUrl('/');
  }

}
