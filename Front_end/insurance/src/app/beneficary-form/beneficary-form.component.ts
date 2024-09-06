import { NgFor } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormBuilder, FormsModule, NgControl, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpCommunicator } from '../HttpCommunication';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-beneficary-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './beneficary-form.component.html',
  styleUrl: './beneficary-form.component.css'
})
export class BeneficaryFormComponent {
  policyid!:number;
  @Input() data: any;
  message?:string;
  

  constructor(private act:ActivatedRoute , private client:HttpCommunicator , private route:Router){}

  ngOnInit(){
    this.act.params.subscribe(x => {this.policyid = x['id']});
  }
  onSubmit():void{
    var response = this.client.buyPolicy(this.policyid);
    response.subscribe({
      next:n => {
        this.message = <string>n.body;  this.showAlert(this.message);
        
      },
      error:e => {
        this.message = e.message;  this.showAlert(this.message);
      }
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
