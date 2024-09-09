import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

  constructor(private router: Router) {}

  loadUsers() {
    this.router.navigate(['/user']);
  }

  loadPolicies() {
    this.router.navigate(['/policy']);
  }

  loadClaims() {
    this.router.navigate(['/claim']);
  }

  loadCarts() {
    this.router.navigate(['/cart']);
  }

}
