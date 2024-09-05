import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { user } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { Observable } from 'rxjs';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule , NgIf],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  existing:string='';
  error:string='';
  userForm!:FormGroup;
  newUser:user = new user();
  message!:string;
  private fb!: FormBuilder;

  //------


  //------

  constructor(private client:HttpCommunicator)
  {
    // this.userForm = this.fb.group({
    //   userID: [null, [Validators.required, Validators.min(1)]],
    //   userName: ['', [Validators.required, Validators.minLength(3)]],
    //   password: ['', [Validators.required, Validators.minLength(6)]],
    //   userType: ['', [Validators.required, Validators.pattern('Admin|Customer')]],
    //   firstName: ['', [Validators.required, Validators.minLength(2)]],
    //   lastName: ['', [Validators.required, Validators.minLength(2)]],
    //   age: [null, [Validators.min(0)]],
    //   gender: ['', [Validators.pattern('M|F')]],
    //   email: ['', [Validators.required, Validators.email]],
    //   contactNo: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
    //   address: ['', [Validators.required]],
    //   customerImage: [null]
    // }); 
  }

  isInvalidAndTouched(controlName: string): boolean {
    var control = this.userForm.get(controlName);
    return control ? control.invalid && control.dirty : false;
  }

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
    this.newUser.userType="customer"
    var response = this.client.Registration(this.newUser);
    response.subscribe({
      error:e =>{this.message = e.message},
      next: n => {this.message = "Account Created!!!"}
    });
  }


}
