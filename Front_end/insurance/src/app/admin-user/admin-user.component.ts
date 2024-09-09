import { Component } from '@angular/core';
import { HttpCommunicator } from '../HttpCommunication';
import { FormsModule } from '@angular/forms';
import { user } from '../models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-user',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './admin-user.component.html',
  styleUrl: './admin-user.component.css'
})
export class AdminUserComponent 
{
  users: user[] = [];
  userDetail: user | null = null;
  searchUsername: string = '';
  offset: number = 0;
  error: string | null = null;

  constructor(private userService: HttpCommunicator) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    var response = this.userService.getUsers();
    response.subscribe({
      next: n => {
        this.users = [...n.body!];
        this.error = null; // Clear any previous errors
      },
      error: e => {
        this.error = e.message;
        this.users = []; // Clear user list on error
      }
    });
  }


  getNextUser(){
    var response = this.userService.getNextUser();
    response.subscribe({
      next: n => {
        this.users = [...n.body!];
        this.error = null; // Clear any previous errors
      },
      error: e => {
        this.error = e.message;
        this.users = []; // Clear user list on error
      }
    });
  }

  searchUser(): void {
      var response = this.userService.searchUser(this.searchUsername);
      response.subscribe({
        next: n => {
          this.userDetail = n.body || null;
          this.error = null; // Clear any previous errors
        },
        error: e => {
          this.userDetail = null;
          this.error = e.message;
        }
      });
    
  }

  deleteUser(username: string): void {
    var response = this.userService.deleteUser(username);
    response.subscribe({
      next: () => this.loadUsers(), // Reload users after deletion
      error: e => this.error = e.message
    });
  }

  updateUser(username: string): void {
    var response = this.userService.updateUser(username);
    response.subscribe({
      next: () => this.loadUsers(), // Reload users after update
      error: e => this.error = e.message
    });
  }
}




