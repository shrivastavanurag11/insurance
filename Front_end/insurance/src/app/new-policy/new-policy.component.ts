import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { HttpCommunicator } from '../HttpCommunication';
import { Policy } from '../models';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-new-policy',
  standalone: true,
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './new-policy.component.html',
  styleUrl: './new-policy.component.css'
})
export class NewPolicyComponent 
{
  policyForm!: FormGroup;
  newPolicy:Policy = new Policy();
  mess:string='';

  constructor(private fb: FormBuilder, private client : HttpCommunicator) { }

  ngOnInit(): void {
    this.policyForm = this.fb.group({
      policyType: ['', [Validators.required]],
      policyName: ['', [Validators.required]],
      insuranceAmount: [0, [Validators.required, Validators.min(0)]],
      policyValidity: [0, [Validators.required, Validators.min(1)]],
      policyDescription: ['']
    });
  }

  onSubmit() {
    if (this.policyForm.valid) {
      this.newPolicy.policyId = 0;
      this.newPolicy.available = 'Y';
      var res = this.client.addNewPolicy(this.newPolicy);
      res.subscribe({
        error:e => {this.mess = e.message; alert(this.mess);},
        next: n => {alert(n.body);}
      });
      // Handle form submission logic here
    } else {
      console.log('Form is not valid');
    }
  }
}
