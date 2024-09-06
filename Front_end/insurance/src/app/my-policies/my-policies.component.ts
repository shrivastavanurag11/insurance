import { Component } from '@angular/core';
import { HttpCommunicator } from '../HttpCommunication';
import { PolicySold } from '../models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-my-policies',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-policies.component.html',
  styleUrl: './my-policies.component.css'
})
export class MyPoliciesComponent {
  message:string='';
  mypolicies!:PolicySold[];
  constructor(private client : HttpCommunicator){

  }
 ngOnInit(){
  var response= this.client.myPolicies();
  response.subscribe({
    error: e => {this.message = e.message;},
    next: n => {this.mypolicies = [...n.body!];}
  });
 }

}
