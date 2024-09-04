import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { user } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  existing:string='';
  error:string='';
  userForm!:FormGroup;
  newUser:user = new user();
  message!:string;

  //------


  //------

  constructor(private client:HttpCommunicator){}

  checkExistingUser(username:string):void{
    this.existing='';

    var response = this.client.checkExistingUser(username);
    response.subscribe({
      next: n => 
      { 
        if(n) {this.existing = "Username already exists!";}

      },
        error:e=>{this.error = e.message;}
    })
  }
  submit():void
  {
    var response = this.client.Registration(this.newUser);
    response.subscribe({
      error:e =>{this.message = e.message},
      next: n => {this.message = "Account Created!!!"}
    });
  }


}
