import { Component } from '@angular/core';
import { user } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-user',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './update-user.component.html',
  styleUrl: './update-user.component.css'
})
export class UpdateUserComponent 
{
  mydetails!:user;
  message:string='';
  isEditing: boolean = false;

  enableEditing() {
    this.isEditing = true;
  }


  constructor(private client:HttpCommunicator , private route:Router)
  {
    var response = client.userDetails();
    response.subscribe({
      error:e => {this.message = e.message},
      next: n => {this.mydetails = <user> n.body}
    });
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.mydetails.customerImage = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }


    submit(updated:user)
    {
      var response = this.client.updateProfile(updated);
      response.subscribe({
        error: e => {this.message = e.message; this.showAlert(this.message);},
        next: n => {this.message = <string> n.body; this.showAlert(this.message);}
      });
    }


    showAlert(message?: string) {
      Swal.fire({
          title: 'Alert',
          text: message,
          icon: 'info',
          confirmButtonText: 'OK'
      });
      this.route.navigate(['customer'])

}
}
